using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{

    public class DoctorController : Controller
    {

        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewPatient()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("VIEW_PATIENT LOG " + userEmail);
            var doctor = _context.doctors //doktoru getir
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(d => d.Email == userEmail);

            //var appointments = _context.appointments.Where(p => p.DoctorID == doctor.ApplicationUser.ApplicationUserID).ToList();
            //System.Console.WriteLine("Appointments Count: " + appointments.Count);
            //var patients = appointments.Select(a => a.Patient).Distinct().ToList(); id cekiyo
            var appointments = _context.appointments
                .Where(a => a.DoctorID == doctor.ApplicationUser.ApplicationUserID) // 
                .Include(a => a.Patient) // Patient gelmesi lazım
                .Select(a => a.Patient) // navigation objelere bak
                .Distinct()
                .ToList();


            System.Console.WriteLine("Patients Count AAAAA: " + appointments.Count);


            return View(appointments);

        }

        [HttpGet] //("Doctor/ViewDiagnosis/{id}")
        public IActionResult ViewDiagnosis(int id)
        {
            System.Console.WriteLine("VIEW DIAGNOSIS DEBUG : " + id);

            var diagnosis = _context.diagnoses.Where(d => d.PatientID == id).ToList();
            int patientId = id;

            var DTO = new DiagnosisDTO
            {
                Diagnoses = diagnosis,
                PatientID = id
            };

            return View(DTO);
        }

        [HttpGet]
        public IActionResult AddDiagnosis(int id)
        {
            var model = new DiagnosisDTO
            {
                PatientID = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDiagnosis(DiagnosisDTO model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("USEREMAİLEMAİLEMAİLEMAİİL ADD DİAGNOSIS POST " + userEmail);
            var doctor = _context.doctors
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("USEREMAİLEMAİLEMAİLEMAİİL ADD DİAGNOSIS POST " + doctor.ApplicationUser.ApplicationUserID);

            Console.WriteLine("PatientID AddDiagnosis POST Request: " + model.PatientID);
            if (ModelState.IsValid)
            {

                var diagnosis = new Diagnosis
                {
                    PatientID = model.PatientID,
                    DoctorID = doctor.ApplicationUser.ApplicationUserID,
                    DiagnosisDescription = model.DiagnosisDescription,
                    DiagnosisDate = model.DiagnosisDate
                };
                _context.diagnoses.Add(diagnosis);
                _context.SaveChanges();
                return RedirectToAction("ViewDiagnosis", "Doctor", new { id = model.PatientID });
            }
            return View(model);
        }

        public IActionResult ViewReport(int id)
        {
            System.Console.WriteLine("VIEW REPORT DEBUG ID:" + id);
            var reports = _context.reports.Where(r => r.PatientID == id).ToList();

            var DTO = new ReportDTO
            {
                Reports = reports,
                PatientID = id
            };

            return View(DTO);
        }

        [HttpPost]
        public IActionResult AddReport(ReportDTO model)
        {
            System.Console.WriteLine("ADD REPORT DEBUG PATIENT ID : " + model.PatientID);
            System.Console.WriteLine("ADD REPORT DEBUG DESC : " + model.ReportDescription);
            System.Console.WriteLine("ADD REPORT DEBUG FileName: " + model.ReportFile.FileName);
            if (ModelState.IsValid)
            {
                // Save the report to the database
                var report = new Report
                {
                    ReportDescription = model.ReportDescription,
                    PatientID = model.PatientID,
                    filename = model.ReportFile.FileName,
                    filepath = UploadFile(model.ReportFile)
                };

                _context.reports.Add(report);
                _context.SaveChanges();

                return RedirectToAction("ViewReport", "Doctor", new { id = model.PatientID });
            }

            return View(model);
        }
        public IActionResult ViewPrescription(int id)
        {
            System.Console.WriteLine("VIEW REPORT DEBUG ID:" + id);
            var prescriptions = _context.prescriptions.Where(r => r.PatientID == id).ToList();

            var DTO = new PrescriptionDTO
            {
                Prescriptions = prescriptions,
                PatientID = id
            };

            return View(DTO);
        }
        [HttpPost]
        public IActionResult AddPrescription(PrescriptionDTO model)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var doctor = _context.doctors
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);

            System.Console.WriteLine("ADD PRES DEBUG PATIENT ID : " + model.PatientID);
            System.Console.WriteLine("ADD PRES DEBUG DESC : " + model.PrescriptionDate);
            System.Console.WriteLine("ADD PRES DEBUG FileName: " + model.PrescriptionFile.FileName);

            if (ModelState.IsValid)
            {
                // Save the report to the database
                var prescription = new Prescription
                {
                    PrescriptionDate = DateTime.Now,
                    DoctorID = doctor.ApplicationUser.ApplicationUserID,
                    PatientID = model.PatientID,
                    filename = model.PrescriptionFile.FileName,
                    filepath = UploadFile(model.PrescriptionFile)
                };

                _context.prescriptions.Add(prescription);
                _context.SaveChanges();

                return RedirectToAction("ViewPrescription", "Doctor", new { id = model.PatientID });
            }

            return View(model);
        }

        public IActionResult ViewRadiologicalReport(int id)
        {
            var RadiologicalReports = _context.radiologicalReports.Where(r => r.PatientID == id).ToList();
            var DTO = new RadiologicalReportDTO
            {
                RadiologicalReports = RadiologicalReports,
                PatientID = id
            };
            return View(DTO);
        }
        [HttpPost]
        public IActionResult AddRadiologicalReport(RadiologicalReportDTO model)
        {
            System.Console.WriteLine("ADD RR DEBUG PATIENT ID : " + model.PatientID);
            System.Console.WriteLine("ADD RR DEBUG DESC : " + model.RrDescription);
            System.Console.WriteLine("ADD RR DEBUG FileName: " + model.RadiologicalReportFile.FileName);

            if (ModelState.IsValid)
            {
                // Save the report to the database
                var rr = new RadiologicalReport
                {
                    RrDescription=model.RrDescription,
                    PatientID = model.PatientID,
                    filename = model.RadiologicalReportFile.FileName,
                    filepath = UploadFile(model.RadiologicalReportFile) // guid eklenmiş versiyonu
                };

                _context.radiologicalReports.Add(rr);
                _context.SaveChanges();

                return RedirectToAction("ViewRadiologicalReport", "Doctor", new { id = model.PatientID });
            }

            return View(model);
        }
        private string UploadFile(IFormFile file)
        {
            string uniqueFileName = null;
            string filePath = null; 

            if (file != null)
            {
                string uploadsFolder = Path.Combine("wwwroot", "uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //directory oluştur
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            //return filePath; unique file name döndürmesi şimdilik kolay
            return uniqueFileName;
        }
        public IActionResult DoctorList()
        {
            var doctors = _context.doctors.Include(d => d.Departments.Hospital).ToList();
            return View(doctors);
        }


        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public ActionResult UpdateDetails()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public ActionResult UpdateDetails(Doctor model)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine(email + "            <<<<<<<<<<<<<<<<<<<<<<<--------------------------------------------- FOR DEBUG");
            var doctor = _context.doctors.FirstOrDefault(c => c.Email == email);
            if (doctor != null)
            {
            doctor.Age = model.Age;
            doctor.Height = model.Height;
            doctor.Weight = model.Weight;
            doctor.Address = model.Address;
            doctor.PhoneNumber = model.PhoneNumber;
            _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("DOCTOR NULL DÖNÜYORSA <----------------------------------------------------------------------");
            }

            return RedirectToAction("Profile", "Doctor");
        }
        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            var doctor = _context.doctors
            .Include(p => p.ApplicationUser)
            .FirstOrDefault(p => p.Email == userEmail);

           

            if (doctor == null)
            {
                return NotFound();
            }

            var profileDTO = new ProfileDTO
            {
                Doctor = doctor
            };

            return View(profileDTO);

        }

    }
}
