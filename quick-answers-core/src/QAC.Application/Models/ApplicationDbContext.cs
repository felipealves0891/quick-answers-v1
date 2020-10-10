using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QAC.Application.Models
{
    public class ApplicationDbContext : DbContext
    { 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Typification> typifications { get; set; }

        public DbSet<IdentificationValidator> validators { get; set; }

        public DbSet<CustomerService> customerServices { get; set; }
    }
}
