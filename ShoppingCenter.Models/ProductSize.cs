﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShoppingCenter.Models
{
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Size { get; set; }
    }
}
