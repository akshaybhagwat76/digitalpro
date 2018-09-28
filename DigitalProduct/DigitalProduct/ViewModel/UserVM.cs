using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DigitalProduct.ViewModel
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public bool IsDeactive { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
      
        [DisplayName("Role Name")]
        public string role_name { get; set; }

        [DisplayName("Default Rows")]
        public int? default_number { get; set; }
    }
    public class MemberChangePassword
    {
        public int MemberId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}