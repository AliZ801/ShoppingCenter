using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShoppingCenter.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Required]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Display(Name = "Product Type Id")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        [Required]
        [Display(Name = "Product Size Id")]
        public int ProductSizeId { get; set; }

        [ForeignKey("ProductSizeId")]
        public ProductSize ProductSize { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
