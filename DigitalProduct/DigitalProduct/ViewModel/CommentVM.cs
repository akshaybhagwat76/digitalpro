using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalProduct.ViewModel
{
    public class CommentVM
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