using Entities.Models;
using Hospital.Controllers;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models // bura düzeltilcek
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //Bu araya configurationlar gelebilir

        public DbSet<Admin> admins { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Diagnosis> diagnoses { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Hospital> hospitals { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Nurse> nurses { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Prescription> prescriptions { get; set; }
        public DbSet<RadiologicalReport> radiologicalReports { get; set; }
        public DbSet<Receptionist> receptionists { get; set; }
        public DbSet<Report> reports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure ApplicationUser and its relationships
            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Nurse>().ToTable("Nurse");
            modelBuilder.Entity<Receptionist>().ToTable("Receptionist");

            modelBuilder.Entity<Hospital>().HasData(
                new Hospital() { HospitalID = 1, HospitalName = "Medical Park", PhoneNum = "123456789", Address = "Kemaliye Caddesi , Borno Mahallesi, No:188", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 2, HospitalName = "Medicana", PhoneNum = "323456789", Address = "Mahmudiye Caddesi , Yılmaz Mahallesi, No:228", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 3, HospitalName = "Medical Point", PhoneNum = "252417788", Address = "İzmiriye Caddesi , Emir Mahallesi, No:728", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 4, HospitalName = "MediX", PhoneNum = "272417788", Address = "Çakır Caddesi , Kültür Mahallesi, No:28", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 5, HospitalName = "Medic Power", PhoneNum = "172417788", Address = "Harbiye Caddesi , Osman Mahallesi, No:58", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 6, HospitalName = "Chicago", PhoneNum = "202417788", Address = "Muradiye Caddesi , Papatya Mahallesi, No:48", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 7, HospitalName = "Yeditepe Hastanesi", PhoneNum = "222417788", Address = "Fatih Caddesi , Gül Mahallesi, No:558", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 8, HospitalName = "Hacettepe Özel", PhoneNum = "212417788", Address = "Selimiye Caddesi , İhsaniye Mahallesi, No:218", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Hospital() { HospitalID = 9, HospitalName = "Izmir Şehir Hastanesi", PhoneNum = "252416788", Address = "Kitabiye Caddesi , Bilgin Mahallesi, No:98", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted }

            );
            modelBuilder.Entity<Department>().HasData(
                //Medical Park
                new Department() { HospitalID = 1, DepartmentID = 1, DepartmentName = "Kardiyoloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 1, DepartmentID = 2, DepartmentName = "Nöroloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 1, DepartmentID = 3, DepartmentName = "Dahiliye", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                //Medicana
                new Department() { HospitalID = 2, DepartmentID = 4, DepartmentName = "Göz Hastalıkları", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 2, DepartmentID = 5, DepartmentName = "Radyoloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 2, DepartmentID = 6, DepartmentName = "Nöroloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                // Departments for MediX Hospital
                new Department() { HospitalID = 4, DepartmentID = 10, DepartmentName = "Psikiyatri", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 4, DepartmentID = 11, DepartmentName = "Dermatoloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 4, DepartmentID = 12, DepartmentName = "Fizik Tedavi ve Rehabilitasyon", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },

                // Departments for Medic Power Hospital
                new Department() { HospitalID = 5, DepartmentID = 13, DepartmentName = "Kulak Burun Boğaz", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 5, DepartmentID = 14, DepartmentName = "Kadın Hastalıkları ve Doğum", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 5, DepartmentID = 15, DepartmentName = "Çocuk Sağlığı ve Hastalıkları", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },

                // Departments for Chicago Hospital
                new Department() { HospitalID = 6, DepartmentID = 16, DepartmentName = "Gastroenteroloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 6, DepartmentID = 17, DepartmentName = "Endokrinoloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 6, DepartmentID = 18, DepartmentName = "Kardiyovasküler Cerrahi", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },

                // Departments for Yeditepe Hastanesi Hospital
                new Department() { HospitalID = 7, DepartmentID = 19, DepartmentName = "Plastik ve Rekonstrüktif Cerrahi", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 7, DepartmentID = 20, DepartmentName = "Nefroloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 7, DepartmentID = 21, DepartmentName = "Kardiyovasküler Anestezi ve Yoğun Bakım", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },

                // Departments for Hacettepe Özel Hospital
                new Department() { HospitalID = 8, DepartmentID = 22, DepartmentName = "Plastik Cerrahi", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 8, DepartmentID = 23, DepartmentName = "Göğüs Cerrahisi", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 8, DepartmentID = 24, DepartmentName = "Kulak Burun Boğaz", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },

                // Departments for Izmir Şehir Hastanesi Hospital
                new Department() { HospitalID = 9, DepartmentID = 25, DepartmentName = "Kardiyoloji", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 9, DepartmentID = 26, DepartmentName = "Ortopedi", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted },
                new Department() { HospitalID = 9, DepartmentID = 27, DepartmentName = "Göz Hastalıkları", CreatedDate = DateTime.Now, DeletedDate = DateTime.MinValue, ModifiedDate = DateTime.MinValue, Status = Enums.DataStatus.Inserted }
            );

            modelBuilder.Entity<ApplicationUser>().HasData( // burada bir sıkıntı var normalde admine ekleme yapılmalı
                new ApplicationUser()
                {                                           // age, weight, height null geliyor
                    ApplicationUserID = -1,
                    Name = "Kerem",
                    Surname = "mereK",
                    Role = "Admin",
                    PhoneNumber = "5554446677",
                    Address = "IYTE Müh F Binası",
                    Gender = "Erkek",
                    BloodType = "A",
                    Email = "admin@hospitaladmin.com",
                    Password = "123",
                    CreatedDate = DateTime.Now,
                    DeletedDate = DateTime.MinValue,
                    ModifiedDate = DateTime.MinValue,
                    Status = Enums.DataStatus.Inserted,
                    Age = 22,
                    Weight = 70,
                    Height = 170
                }
            );






            // // ONE DOCTOR MANY PATIENT --silmeyi deniyorum

            // modelBuilder.Entity<Doctor>() 
            //  .HasMany(a => a.Patients)
            //  .WithOne(e => e.Doctor)
            //  .HasForeignKey(a => a.DoctorID);

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

            // Ref: Appointment.docID - Doctor.docID                    // ONE APPOINTMENT ONE DOCTOR // many ile değiştirildi

            modelBuilder.Entity<Doctor>()
           .HasMany(doc => doc.Appointments)
           .WithOne(app => app.Doctor)
           .HasForeignKey(a => a.DoctorID)
           .OnDelete(DeleteBehavior.Restrict);

            // Ref: Appointment.patientID - Patient.patientID           // ONE APPOINTMENT ONE PATIENT // many ile değiştirildi

            modelBuilder.Entity<Patient>() // değiştiriyom
            .HasMany(p => p.Appointments)
            .WithOne(x => x.Patient)
            .HasForeignKey(x => x.PatientID)
            .OnDelete(DeleteBehavior.Restrict);



            // Ref: Department.departmentID - Nurse.departmentID        // ONE NURSE ONE DEPARTMENT // many ile değiştirildi

            modelBuilder.Entity<Department>()
            .HasMany(n => n.Nurses)
            .WithOne(dep => dep.Department)
            .HasForeignKey(n => n.DepartmentID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Doctor.docID > Prescription.docID                   // ONE DOCTOR MANY PRESCRIPTIONS

            modelBuilder.Entity<Doctor>()
            .HasMany(doc => doc.Prescriptions)
            .WithOne(pr => pr.Doctor)
            .HasForeignKey(pr => pr.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Doctor.docID < Diagnosis.docID                      // ONE DOCTOR MANY DIAGNOSES

            modelBuilder.Entity<Doctor>()
            .HasMany(doc => doc.Diagnoses)
            .WithOne(diag => diag.Doctor)
            .HasForeignKey(diag => diag.DoctorID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Diagnosis.patientID > Patient.patientID             // MANY DIAGNOSES ONE PATIENT

            modelBuilder.Entity<Diagnosis>()
            .HasOne(diag => diag.Patient)
            .WithMany(pat => pat.Diagnoses)
            .HasForeignKey(diag => diag.PatientID)
            .OnDelete(DeleteBehavior.Restrict);

            // Ref: Report.diagnosisID - Diagnosis.diagnosisID          // ONE REPORT ONE DIAGNOSIS

            // modelBuilder.Entity<Report>() //kaldırıldı, duruma göre yeniden değerlendirilebilir mi 
            // .HasOne(dia => dia.Diagnosis)
            // .WithOne(rep => rep.Reports)
            // .HasForeignKey<Diagnosis>(rep => rep.DiagnosisID)
            // .OnDelete(DeleteBehavior.Restrict);

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