using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalProduct.ViewModel
{
    public class ProductVM
    {
        public int Id { get; set; }

        [DisplayName("Product")]
        [StringLength(128, ErrorMessage = "Max 128 characters")]
        public string ProductName { get; set; }
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Description")]
        [StringLength(100, ErrorMessage = "Max 256 characters")]
        public string Description { get; set; }

        [DisplayName("Price")]
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public bool Status { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }

    }
}