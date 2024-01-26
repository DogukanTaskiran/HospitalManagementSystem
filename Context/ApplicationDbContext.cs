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
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Configure ApplicationUser and its relationships
        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        modelBuilder.Entity<Admin>().ToTable("Admin");
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<Patient>().ToTable("Patient");

        
        modelBuilder.Entity<ApplicationUser>()// one to one appuser - admin
            .HasOne(u => u.Admin)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Admin>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ApplicationUser>() // one to one appuser -patient
            .HasOne(u=>u.Patient)
            .WithOne(b =>b.ApplicationUser)
            .HasForeignKey<Patient>(a=>a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        modelBuilder.Entity<ApplicationUser>()// one to one appuser - doctor
            .HasOne(u=>u.Doctor)
            .WithOne(a=>a.ApplicationUser)
            .HasForeignKey<Doctor>(a=>a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Room>()//room -patient
            .HasOne(u=>u.Patient)
            .WithOne(a=>a.Room)
            .HasForeignKey<Patient>(e=>e.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Department>()//doctor department
            .HasMany(e=>e.Doctor)
            .WithOne(u=>u.Department)
            .HasForeignKey(a=>a.DepartmentID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Doctor>() //doctor visits
            .HasMany(e=>e.Visits)
            .WithOne(u=>u.Doctor)
            .HasForeignKey(a=>a.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Patient>() // patient visits
            .HasMany(e=>e.Visits)
            .WithOne(u=>u.Patient)
            .HasForeignKey(a=>a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Patient>() // patient invoices
            .HasMany(a=>a.Invoices)
            .WithOne(e=>e.Patient)
            .HasForeignKey(a=>a.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Patient>() // patient -prescription
            .HasMany(a=>a.Prescriptions)
            .WithOne(b=>b.Patient)
            .HasForeignKey(c=>c.PatientID)
            .OnDelete(DeleteBehavior.Restrict);
        // modelBuilder.Entity<Patient>()
        //     .HasOne(p => p.Doctor)
        //     .WithMany(d => d.Patients)
        //     .HasForeignKey(p => p.DoctorID)
        //     .OnDelete(DeleteBehavior.NoAction);
        
        

        // Call the base OnModelCreating method
        base.OnModelCreating(modelBuilder);
    }


    }
}