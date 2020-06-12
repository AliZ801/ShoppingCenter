using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCenter.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public string Type { get; set; }
    }
}
