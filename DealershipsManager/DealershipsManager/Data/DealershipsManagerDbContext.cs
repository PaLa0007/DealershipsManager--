using System;
using System.Collections.Generic;
using System.Text;
using DealershipsManager.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DealershipsManager.Data
{
        public class DealershipsManagerDbContext : IdentityDbContext
        {
            public DealershipsManagerDbContext()
            {

            }

            public DealershipsManagerDbContext(DbContextOptions<DealershipsManagerDbContext> options)
                : base(options)
            {

            }

            public DbSet<Dealership> Dealerships { get; set; }

            public DbSet<Car> Cars { get; set; }

            public DbSet<User> Users { get; set; }
        }
    
}
