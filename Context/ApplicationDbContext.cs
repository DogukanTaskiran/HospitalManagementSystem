using Entities.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {
        
        }

        //Bu araya configurationlar gelebilir

        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }

        public DbSet<Admin> admins { get; set; }

        public DbSet<Hospital> hospitals { get; set;}

        public DbSet<Department> departments { get; set; }

        public DbSet<Room> rooms { get; set; }

        public DbSet<Visit> visits { get; set; }

        public DbSet<Invoice> invoices { get; set; }

        public DbSet<Prescription> prescriptions { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionString");
        //}


    }
}