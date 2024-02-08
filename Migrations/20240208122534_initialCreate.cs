using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hospitals",
                columns: table => new
                {
                    HospitalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hospitals", x => x.HospitalID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ApplicationUserID);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_departments_hospitals_HospitalID",
                        column: x => x.HospitalID,
                        principalTable: "hospitals",
                        principalColumn: "HospitalID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_Admin_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_Patient_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_Doctor_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctor_departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nurse",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    NurseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurse", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_Nurse_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nurse_departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receptionist",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    ReceptionistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptionist", x => x.ApplicationUserID);
                    table.ForeignKey(
                        name: "FK_Receptionist_User_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "User",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receptionist_departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoicePrice = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_invoices_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "radiologicalReports",
                columns: table => new
                {
                    RadiologicalReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    RrDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filepath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_radiologicalReports", x => x.RadiologicalReportID);
                    table.ForeignKey(
                        name: "FK_radiologicalReports_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ReportDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filepath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_reports_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: true),
                    PatientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK_appointments_Doctor_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctor",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appointments_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "diagnoses",
                columns: table => new
                {
                    DiagnosisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiagnosisDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnoses", x => x.DiagnosisID);
                    table.ForeignKey(
                        name: "FK_diagnoses_Doctor_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctor",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_diagnoses_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PrescriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filepath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_prescriptions_Doctor_DoctorID",
                        column: x => x.DoctorID,
                        principalTable: "Doctor",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prescriptions_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ApplicationUserID", "Address", "Age", "BloodType", "CreatedDate", "DeletedDate", "Email", "Gender", "Height", "ModifiedDate", "Name", "Password", "PhoneNumber", "Role", "Status", "Surname", "Weight" },
                values: new object[] { -1, "IYTE Müh F Binası", 22, "A", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8445), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@hospitaladmin.com", "Erkek", 170, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kerem", "123", "5554446677", "Admin", 1, "mereK", 70 });

            migrationBuilder.InsertData(
                table: "hospitals",
                columns: new[] { "HospitalID", "Address", "CreatedDate", "DeletedDate", "HospitalName", "ModifiedDate", "PhoneNum", "Status" },
                values: new object[,]
                {
                    { 1, "Kemaliye Caddesi , Borno Mahallesi, No:188", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8220), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medical Park", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123456789", 1 },
                    { 2, "Mahmudiye Caddesi , Yılmaz Mahallesi, No:228", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8224), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medicana", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "323456789", 1 },
                    { 3, "İzmiriye Caddesi , Emir Mahallesi, No:728", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8226), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medical Point", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "252417788", 1 },
                    { 4, "Çakır Caddesi , Kültür Mahallesi, No:28", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8227), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MediX", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "272417788", 1 },
                    { 5, "Harbiye Caddesi , Osman Mahallesi, No:58", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8229), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medic Power", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "172417788", 1 },
                    { 6, "Muradiye Caddesi , Papatya Mahallesi, No:48", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8231), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chicago", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "202417788", 1 },
                    { 7, "Fatih Caddesi , Gül Mahallesi, No:558", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8232), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yeditepe Hastanesi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "222417788", 1 },
                    { 8, "Selimiye Caddesi , İhsaniye Mahallesi, No:218", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8234), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hacettepe Özel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "212417788", 1 },
                    { 9, "Kitabiye Caddesi , Bilgin Mahallesi, No:98", new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8235), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Izmir Şehir Hastanesi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "252416788", 1 }
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "DepartmentID", "CreatedDate", "DeletedDate", "DepartmentName", "HospitalID", "ModifiedDate", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8370), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kardiyoloji", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8372), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nöroloji", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8375), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dahiliye", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8376), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Göz Hastalıkları", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8379), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Radyoloji", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8380), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nöroloji", 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 10, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8382), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Psikiyatri", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 11, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8384), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dermatoloji", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8386), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fizik Tedavi ve Rehabilitasyon", 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8387), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kulak Burun Boğaz", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 14, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8389), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kadın Hastalıkları ve Doğum", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 15, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8390), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çocuk Sağlığı ve Hastalıkları", 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 16, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8391), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gastroenteroloji", 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 17, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8393), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Endokrinoloji", 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 18, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8394), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kardiyovasküler Cerrahi", 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 19, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8396), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plastik ve Rekonstrüktif Cerrahi", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 20, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8397), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nefroloji", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 21, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8398), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kardiyovasküler Anestezi ve Yoğun Bakım", 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 22, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8401), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Plastik Cerrahi", 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 23, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8402), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Göğüs Cerrahisi", 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 24, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8404), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kulak Burun Boğaz", 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 25, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8405), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kardiyoloji", 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 26, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8406), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ortopedi", 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 27, new DateTime(2024, 2, 8, 15, 25, 33, 917, DateTimeKind.Local).AddTicks(8408), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Göz Hastalıkları", 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_DoctorID",
                table: "appointments",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_PatientID",
                table: "appointments",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_departments_HospitalID",
                table: "departments",
                column: "HospitalID");

            migrationBuilder.CreateIndex(
                name: "IX_diagnoses_DoctorID",
                table: "diagnoses",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_diagnoses_PatientID",
                table: "diagnoses",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_DepartmentID",
                table: "Doctor",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_invoices_PatientID",
                table: "invoices",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurse_DepartmentID",
                table: "Nurse",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_DoctorID",
                table: "prescriptions",
                column: "DoctorID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_PatientID",
                table: "prescriptions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_radiologicalReports_PatientID",
                table: "radiologicalReports",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Receptionist_DepartmentID",
                table: "Receptionist",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_reports_PatientID",
                table: "reports",
                column: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "diagnoses");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "radiologicalReports");

            migrationBuilder.DropTable(
                name: "Receptionist");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "hospitals");
        }
    }
}
