using AutoMapper;
using DigitalProduct.Helpers;
using DigitalProduct.Models;
using DigitalProduct.ViewModel;
using Elmah;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DigitalProduct.Repository
{
    public class UserRepository
    {
        DigitalProductEntities db = new DigitalProductEntities();

        private IMessaging messenger;

        public UserRepository()
        {

        }

        public UserRepository(IMessaging _messenger)
        {
            messenger = _messenger;
        }

        public List<UserVM> GetAll()
        {
            List<userprofile> user = new List<userprofile>();
            try
            {
                user = db.userprofiles.Where(i => !i.IsDeactive).ToList();
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<List<userprofile>, List<UserVM>>(user);
        }

        public UserVM GetProfile(int id)
        {
            userprofile user = new userprofile();
            try
            {
                user = db.userprofiles.FirstOrDefault(i => i.UserId == id && !i.IsDeactive);
                if (user == null)
                {
                    throw new Exception(Messages.BAD_DATA);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            return Mapper.Map<userprofile, UserVM>(user);
        }

        public UserVM Login(UserVM loginVM)
        {
            userprofile user = new userprofile();
            try
            {
                if (string.IsNullOrWhiteSpace(loginVM.Email))
                {
                    throw new Exception(Messages.BAD_DATA);
                }
                user = db.userprofiles.FirstOrDefault(x => x.Email == loginVM.Email && x.Password == loginVM.Password && !x.IsDeactive);
                if (user == null)
                {
                    throw new Exception(Messages.INVALID_USER_PASS);
                }
                if (user.IsDeactive)
                {
                    throw new Exception(Messages.NOT_ACTIVE);
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
            var _data = Mapper.Map<userprofile, UserVM>(user);
            _data.role_name = db.user_role.FirstOrDefault(r => r.Id == _data.RoleId).RoleName;
            return _data;
        }

        public bool IsUserExists(string email, int id)
        {
            return db.userprofiles.Count(x => x.Email == email && x.UserId != id) > 0;
        }

        public bool IsUserExists(string email)
        {
            return db.userprofiles.Count(x => x.Email == email) > 0;
        }

        public UserVM GetUserByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new Exception(Messages.BAD_DATA);
                }
                var user = db.userprofiles.FirstOrDefault(x => x.Email == email && !x.IsDeactive);
                return Mapper.Map<userprofile, UserVM>(user);
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        public UserVM AddOrUpdate(UserVM userVM)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userVM.Email))
                    throw new Exception(Messages.BAD_DATA);

                var user = db.userprofiles.FirstOrDefault(x => x.UserId == userVM.UserId);
                //if (!string.IsNullOrEmpty(userVM.profile_pic))
                //    new_profile_pic = new Helpers.Common().SaveBase64AsImage(userVM.profile_pic, AppFolderPaths.ProfileImageFolder);
                if (user == null)
                {
                    if (!string.IsNullOrWhiteSpace(userVM.Email) && IsUserExists(userVM.Email))
                        throw new Exception(Messages.USER_EXISTS);

                    user = Mapper.Map<UserVM, userprofile>(userVM);
                    user.RoleId = 2;
                    user.CreatedDate = DateTime.UtcNow;
                    db.userprofiles.Add(user);
                    db.SaveChanges();
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(userVM.Email) && IsUserExists(userVM.Email, userVM.UserId))
                        throw new Exception(Messages.USER_EXISTS);
                    userVM.CreatedDate = user.CreatedDate;
                    user.RoleId = 2;
                    userVM.IsDeactive = user.IsDeactive;
                    db.Entry(user).CurrentValues.SetValues(userVM);

                    db.SaveChanges();
                }


                return Mapper.Map<userprofile, UserVM>(user);
            }
            catch (DbEntityValidationException dve)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(dve);
                throw new Exception(string.Join("\n", dve.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(y => y.ErrorMessage)));
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        public string ForgotPassword(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new Exception(Messages.BAD_DATA);
                }
                var user = db.userprofiles.FirstOrDefault(x => x.Email == email && !x.IsDeactive);
                if (user == null)
                {
                    throw new Exception(Messages.BAD_DATA);
                }
                if (messenger == null) throw new Exception(Messages.ERROR_SENDING_EMAIL);
                var messagedata = new
                {
                    email = user.Email,
                    url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority,
                    token = HttpContext.Current.Server.UrlEncode(Security.Encrypt(user.UserId.ToString()))
                };
                messenger.SendEmail(user.Email, Messages.PASSWORD_RESET, string.Format(Messages.PASSWORD_RESET_MESSAGE, messagedata.email, messagedata.url, messagedata.token));
                return Messages.PASSWORD_RESET_SUCCESS;
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null) ErrorSignal.FromCurrentContext().Raise(ex);
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool ResetPassword(MemberChangePassword resetpassword)
        {
            try
            {
                var _getpassword = db.userprofiles.Where(x => x.UserId == resetpassword.MemberId).FirstOrDefault();
                if (_getpassword.Password == resetpassword.OldPassword)
                {
                    _getpassword.Password = resetpassword.NewPassword;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public bool Delete(int id)
        {
            var data = db.userprofiles.FirstOrDefault(x => x.UserId == id);
            if (data != null)
            {
                data.IsDeactive = true;
            }
            return db.SaveChanges() > 0;
        }
    }
}