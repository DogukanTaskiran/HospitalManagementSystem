using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class initalCreate : Migration
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
                    PhoneNum = table.Column<int>(type: "int", nullable: false),
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ApplicationUserID);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: false)
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
                name: "Nurse",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    NurseID = table.Column<int>(type: "int", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
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
                name: "Receptionist",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    RecID = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalID = table.Column<int>(type: "int", nullable: false),
                    NurseID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_departments_Nurse_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Nurse",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_departments_hospitals_HospitalID",
                        column: x => x.HospitalID,
                        principalTable: "hospitals",
                        principalColumn: "HospitalID",
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
                    RrImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    DiagnosisID = table.Column<int>(type: "int", nullable: false),
                    ReportDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "Doctor",
                columns: table => new
                {
                    ApplicationUserID = table.Column<int>(type: "int", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
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
                name: "appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DoctorID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
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
                    DiagnosisID = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_diagnoses_reports_DiagnosisID",
                        column: x => x.DiagnosisID,
                        principalTable: "reports",
                        principalColumn: "ReportID",
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
                name: "Nurse");

            migrationBuilder.DropTable(
                name: "hospitals");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
