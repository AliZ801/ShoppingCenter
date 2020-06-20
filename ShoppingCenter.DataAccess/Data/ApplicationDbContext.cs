using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCenter.Models;

namespace ShoppingCenter.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<ProductSize> ProductSize { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<OrderHeader> OrderHeader { get; set; }

        public DbSet<OrderDetails> OrderDetail { get; set; }
    }
}
