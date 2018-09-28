using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalProduct.ViewModel
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }

    }
}