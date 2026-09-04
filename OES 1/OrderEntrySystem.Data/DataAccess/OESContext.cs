using Microsoft.EntityFrameworkCore;
using OrderEntrySystem.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderEntrySystem.Data.DataAccess
{
    public class OESContext : DbContext
    {
        public OESContext(DbContextOptions<OESContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
