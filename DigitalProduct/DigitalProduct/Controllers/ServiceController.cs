using DigitalProduct.Helpers;
using DigitalProduct.Repository;
using DigitalProduct.ViewModel;
using Elmah;
using System;
using System.Web.Http;

namespace DigitalProduct.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(UserVM _login)
        {
            try
            {
                var UserExist = new UserRepository().Login(_login);
                if (UserExist != null)
                {
                    return Success(Messages.SUCCESS, UserExist);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
            }
        }

        public IHttpActionResult AddOrUpdateUser(UserVM user)
        {
            try
            {
                var userobj = new UserRepository().AddOrUpdate(user);
                if (userobj != null)
                {
                    return Success(Messages.SUCCESS, userobj);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
            }
        }

        public IHttpActionResult AddOrUpdateComment(CommentVM comment)
        {
            try
            {
                var commentobj = new CommentRepository().AddOrUpdate(comment);
                if (commentobj != null)
                {
                    return Success(Messages.SUCCESS, commentobj);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
                throw;
            }
        }

        public IHttpActionResult GetUserProfile(int id)
        {
            try
            {
                var userobj = new UserRepository().GetProfile(id);
                if (userobj != null)
                {
                    return Success(Messages.SUCCESS, userobj);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
            }
        }


        [HttpPost]
        public IHttpActionResult GetProductComments(int product_id)
        {
            try
            {
                var comment = new CommentRepository().CommentsForProduct(product_id);
                if (comment != null)
                {
                    return Success(Messages.SUCCESS, comment);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
            }
        }


        // Reset Password
        public IHttpActionResult ResetPassword(MemberChangePassword resetpassword)
        {
            try
            {
                var userobj = new UserRepository().ResetPassword(resetpassword);
                if (userobj == true)
                {
                    return Success(Messages.SUCCESS);
                }
                return Error(Messages.FAIL);
            }
            catch (Exception ex)
            {
                return Error(ex.Message.ToString());
            }
        }
        #region ------------------ Reply Format ----------------------

        public IHttpActionResult Success(string txt, dynamic data = null)
        {
            return PrepareReply(false, txt, data);
        }

        public IHttpActionResult Error(string txt)
        {
            return PrepareReply(true, txt);
        }

        public IHttpActionResult PrepareReply(bool isError, string txt, dynamic data = null)
        {
            var reply = new Reply
            {
                status = isError ? Messages.FAIL : Messages.SUCCESS,
                msg = isError ? "" : txt,
                error = isError ? txt : null,
                data = data,
            };
            return Ok(reply);
        }

        public class Reply
        {
            public string status { get; set; }
            public string msg { get; set; }
            public string error { get; set; }
            public dynamic data { get; set; }
        }

        #endregion
    }
}