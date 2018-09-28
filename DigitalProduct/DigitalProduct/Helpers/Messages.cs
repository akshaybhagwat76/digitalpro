using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalProduct.Helpers
{
    public class Messages
    {
        public const string FAIL = "Fail";
        public const string SUCCESS = "Success";
        public const string ALREADY_EXISTS = "Already exists";

        public const string BAD_DATA = "Bad or Invalid data";
        public const string USER_EXISTS = "User already exists";
        public const string INVALID_USER_PASS = "Invalid email or password";
        public const string NOT_ACTIVE = "Account not active";

        public const string ERROR_SENDING_EMAIL = "Error sending email";

        public const string PASSWORD_RESET = "Reset Password Request";
        public const string PASSWORD_RESET_SUCCESS = "Please check your email for reset password";

        public const string PASSWORD_RESET_MESSAGE = "Hello,<br/><br/>We received a request to reset the password for your Account {0}.<br/><br/>Please click on the following link <a href= '{1}/Account/resetpassword?token={2}' target= '_blank' >{1}/Account/resetpassword?token={2}</a> <br/><br/>Please contact us if you have any problems with your login.<br/><br/>Thank you";

        public const string CURRENT_PASSWORD_MESSAGE = "Current password is wrong";

        public const string ADD_MESSAGE = "Successfully added {0} detail";

        public const string UPDATE_MESSAGE = "Successfully updated {0} detail";

        public const string DELETE_MESSAGE = "Successfully deleted {0} detail";
    }
}