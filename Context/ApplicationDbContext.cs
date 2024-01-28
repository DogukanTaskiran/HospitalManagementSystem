using Entities.Models;
using Hospital.Controllers;
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

        public DbSet<Admin> admins { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Appointment> appointments{ get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Diagnosis> diagnoses{ get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Hospital> hospitals { get; set;}
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Nurse> nurses{ get; set; }
        public DbSet<Patient> patients{ get; set; }
        public DbSet<Prescription> prescriptions { get; set; }
        public DbSet<RadiologicalReport> radiologicalReports{ get; set; }
        public DbSet<Receptionist> receptionists{ get; set; }
        public DbSet<Report> reports{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // Configure ApplicationUser and its relationships
        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        modelBuilder.Entity<Admin>().ToTable("Admin");
        modelBuilder.Entity<Doctor>().ToTable("Doctor");
        modelBuilder.Entity<Patient>().ToTable("Patient");
        modelBuilder.Entity<Nurse>().ToTable("Nurse");
        modelBuilder.Entity<Receptionist>().ToTable("Receptionist");


            // ONE DOCTOR MANY PATIENT

            modelBuilder.Entity<Doctor>() 
             .HasMany(a => a.Patients)
             .WithOne(e => e.Doctor)
             .HasForeignKey(a => a.DocID);

            // one to one appuser - admin

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Admin)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Admin>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

            // one to one appuser - doc

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Doctor)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Doctor>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

            // one to one appuser - nurse

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Nurse)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Nurse>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

            // one to one appuser - patient

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Patient)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Patient>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

            // one to one appuser - rec

            modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Receptionist)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Receptionist>(a => a.ApplicationUserID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Hospital.hospitalID < Department.hospitalID         // ONE HOSPITAL MANY DEPARTMENTS

            modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Departments)
            .WithOne(d => d.Hospital)
            .HasForeignKey(d => d.HospitalID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Department.departmentID < Doctor.departmentID       // ONE DEPARTMENT MANY DOCTORS

            modelBuilder.Entity<Department>()
            .HasMany(d => d.Doctors)
            .WithOne(doc => doc.Departments)
            .HasForeignKey(doc => doc.DepartmentID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Prescription.patientID > Patient.patientID          // ONE PATIENT MANY PRESCRIPTIONS

            modelBuilder.Entity<Patient>()
            .HasMany(p => p.Prescriptions)
            .WithOne(pr => pr.Patient)
            .HasForeignKey(pr => pr.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Invoice.patientID > Patient.patientID               // ONE PATIENT MANY INVOICES

            modelBuilder.Entity<Patient>()
            .HasMany(p => p.Invoices)
            .WithOne(inv => inv.Patient)
            .HasForeignKey(inv => inv.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Appointment.docID - Doctor.docID                    // ONE APPOINTMENT ONE DOCTOR

            modelBuilder.Entity<Appointment>()
           .HasOne(a => a.Doctor)
           .WithOne(doc => doc.Appointments)
           .HasForeignKey<Doctor>(a => a.DocID)
           .OnDelete(DeleteBehavior.Restrict);

            // Ref: Appointment.patientID - Patient.patientID           // ONE APPOINTMENT ONE PATIENT

            modelBuilder.Entity<Appointment>()
            .HasOne(p => p.Patient)
            .WithOne(x => x.Appointment)
            .HasForeignKey<Patient>(x => x.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            /* modelBuilder.Entity<Student>()
              .HasOne<StudentAddress>(s => s.Address)
              .WithOne(ad => ad.Student)
              .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId); */

            // Ref: Department.departmentID - Nurse.departmentID        // ONE NURSE ONE DEPARTMENT

            modelBuilder.Entity<Nurse>()
            .HasOne(n => n.Department)
            .WithOne(dep => dep.Nurse)
            .HasForeignKey<Department>(n => n.DepartmentID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Doctor.docID > Prescription.docID                   // ONE DOCTOR MANY PRESCRIPTIONS

            modelBuilder.Entity<Doctor>()
            .HasMany(doc => doc.Prescriptions)
            .WithOne(pr => pr.Doctor)
            .HasForeignKey(pr => pr.DocID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Doctor.docID < Diagnosis.docID                      // ONE DOCTOR MANY DIAGNOSES

            modelBuilder.Entity<Doctor>()
            .HasMany(doc => doc.Diagnoses)
            .WithOne(diag => diag.Doctor)
            .HasForeignKey(diag => diag.DocID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Diagnosis.patientID > Patient.patientID             // MANY DIAGNOSES ONE PATIENT

            modelBuilder.Entity<Diagnosis>()
            .HasOne(diag => diag.Patient)
            .WithMany(pat => pat.Diagnoses)
            .HasForeignKey(diag => diag.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Report.diagnosisID - Diagnosis.diagnosisID          // ONE REPORT ONE DIAGNOSIS

            modelBuilder.Entity<Report>()
            .HasOne(dia => dia.Diagnosis)
            .WithOne(rep => rep.Reports)
            .HasForeignKey<Diagnosis>(rep => rep.DiagnosisID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Patient.patientID < Report.patientID                // ONE PATIENT MANY REPORTS

            modelBuilder.Entity<Patient>()
            .HasMany(pat => pat.Reports)
            .WithOne(rep => rep.Patient)
            .HasForeignKey(rep => rep.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Patient.patientID < RadiologicalReports.patientID     // ONE PATIENT MANY RADIOLOGICALREPORTS

            modelBuilder.Entity<Patient>()
            .HasMany(pat => pat.RadiologicalReports)
            .WithOne(radRep => radRep.Patient)
            .HasForeignKey(radRep => radRep.PatientID)
            .OnDelete(DeleteBehavior.Restrict);


            // Call the base OnModelCreating method
            base.OnModelCreating(modelBuilder);
    }


    }
}