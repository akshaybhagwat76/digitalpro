//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalProduct.Models
{
    using System;
    
    public partial class sp_getComments_Result
    {
        public int Id { get; set; }
        public string Comment_content { get; set; }
        public System.DateTime Created_date { get; set; }
        public Nullable<System.DateTime> Update_date { get; set; }
        public bool Publish { get; set; }
        public Nullable<int> User_id { get; set; }
        public Nullable<int> Product_id { get; set; }
    }
}
