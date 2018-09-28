using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using DigitalProduct.Repository;
using Elmah;
using System.Collections.Generic;
using DigitalProduct.ViewModel;
using DigitalProduct.Helpers;

namespace DigitalProduct.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;
                int userId = Convert.ToInt32(identity.Claims.Where(c => c.Type == ClaimTypes.Sid)
                       .Select(c => c.Value).SingleOrDefault());
                var user = (dynamic)null;
                if (userId > 0)
                {
                    user = new UserRepository().GetProfile(userId);
                }
                if (Request.IsAuthenticated && userId > 0 && user != null)
                {
                    return Redirect(returnUrl ?? "/Home/Index");
                }
                ViewBag.returnUrl = returnUrl;
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserVM loginVM)
        {
            try
            {
                var user = new UserRepository().Login(loginVM);
                Session["UserId"] = user.UserId;
                SignInUser(user.Email, user.UserId.ToString(), user.role_name);
                return new ReplyFormat().Success(Messages.SUCCESS, null);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddOrUpdateUser(UserVM userVM)
        {
            try
            {
                var user = new UserRepository().AddOrUpdate(userVM);
                SignInUser(user.Email, user.UserId.ToString(), user.role_name);
                return new ReplyFormat().Success(Messages.SUCCESS, user);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            try
            {
                string Msg = new UserRepository(new Messaging()).ForgotPassword(Email);
                return new ReplyFormat().Success(Messages.PASSWORD_RESET_SUCCESS, null);
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message.ToString());
            }
        }

        public ActionResult ResetPassword(string token)
        {
            ViewBag.Message = "";
            UserVM userVM = new UserVM();
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    string TokenDescrypt = Security.Decrypt(token);
                    int userId = 0;
                    Int32.TryParse(TokenDescrypt, out userId);

                    var objuser = new UserRepository().GetProfile(userId);
                    Session["ResetUserId"] = objuser.UserId;
                    userVM.Email = objuser.Email;
                }
                catch (Exception ex)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex);
                    ViewBag.Message = ex.Message;
                }
            }
            return View(userVM);
        }
        [HttpPost]
        public ActionResult ResetPassword(UserVM userVM)
        {
            try
            {
                ViewBag.Message = "";
                int ResetUserId = 0;
                if (Session["ResetUserId"] != null)
                    int.TryParse(Session["ResetUserId"].ToString(), out ResetUserId);
                var user = new UserRepository().GetUserByEmail(userVM.Email);
                if (user != null && user.UserId == ResetUserId)
                {
                    user.Password = userVM.Password;

                    var data = new UserRepository().AddOrUpdate(user);
                    SignInUser(data.Email, data.UserId.ToString(), data.role_name);
                    return new ReplyFormat().Success(Messages.SUCCESS, data);
                }
                else
                {
                    return new ReplyFormat().Error(Messages.BAD_DATA);
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                return new ReplyFormat().Error(ex.Message);
            }
        }

        private void SignInUser(string useremail, string userid, string role_name)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Sid, userid));
            claims.Add(new Claim(ClaimTypes.Email, useremail));
            claims.Add(new Claim(ClaimTypes.Role, role_name.ToLower()));
            var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            // Sign In.    
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, claimIdenties);
        }

        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var auth = ctx.Authentication;
            auth.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}