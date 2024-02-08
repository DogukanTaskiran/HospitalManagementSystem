using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Hospital.Controllers
{
    [Authorize(Policy = "PatientPolicy")]
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IActionResult ViewAppointment()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("USEREMAİLEMAİLEMAİLEMAİİL" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("USEREMAİLEMAİLEMAİLEMAİİL" + patient.PatientID);
            var appointments = _context.appointments.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID && p.Status != Entities.Enums.DataStatus.Deleted).ToList();//patientin application user id'si appointmenttaki patient
            foreach (var app in appointments)
            {
                System.Console.WriteLine("AppointmentList LOg : " + app.AppointmentTime);
            }

            return View(appointments);
        }

    




        [HttpPost]
        public IActionResult DeleteAppointment(int id)
        {
            System.Console.WriteLine("DELETE İÇİN GELEN ID ---->" + id);
            var appointments = _context.appointments.FirstOrDefault(d => d.AppointmentID == id);
            appointments.DeletedDate = DateTime.Now;
            appointments.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewAppointment", "Patient");

        }

        [HttpGet]
        public IActionResult GetDepartments(int hospitalId)
        {
            var departments = _context.departments
                .Where(d => d.HospitalID == hospitalId)
                .ToList();

            return Json(departments);
        }

        [HttpGet]
        public IActionResult GetDoctors(int departmentId)
        {
            Console.WriteLine("Received departmentId: " + departmentId);
            var doctors = _context.doctors
                .Where(d => d.DepartmentID == departmentId)
                .Select(d => new
                {
                    DoctorID = d.DoctorID,
                    Name = d.Name,
                    Surname = d.Surname
                })
                .ToList(); ;

            Console.WriteLine("Number of doctors retrieved: " + doctors.Count);
            return Json(doctors);
        }

        [HttpGet]
        public IActionResult SearchAppointment()
        {
            var hospitals = _context.hospitals.ToList();
            var departments = _context.departments.ToList();
            var doctors = _context.doctors.ToList();

            var dto = new AppointmentDTO
            {
                Hospitals = hospitals,
                Departments = departments,
                Doctors = doctors
            };

            return View(dto);
        }
        [HttpPost]
        public IActionResult SearchAppointment(int hospitalId, int departmentId, int doctorId, DateTime appointmentDate)
        {

            System.Console.WriteLine("DATEEEEEEEEEEEEEEEEEEEEE:" + appointmentDate);
            System.Console.WriteLine("DOCTORIDDDDD:" + doctorId);
            var startTime = appointmentDate.Date.AddHours(9);
            var endTime = appointmentDate.Date.AddHours(17);
            var allTimeSlots = new List<DateTime>();
            var currentTime = startTime;
            while (currentTime < endTime)
            {
                allTimeSlots.Add(currentTime);
                currentTime = currentTime.AddMinutes(30); // 30-minute intervals
            }

            var appid = _context.appointments
               .Include(p => p.Doctor).ThenInclude(a => a.ApplicationUser)
               .Where(a => a.Doctor.DoctorID == doctorId)
               .Select(a => a.Doctor.ApplicationUserID)
               .FirstOrDefault();

            Console.WriteLine("Booked Appointments:");
            Console.WriteLine($"ApplicationUserID: {appid}");

            var bookedAppointments = _context.appointments.Where(a => a.DoctorID == appid && a.AppointmentDate == appointmentDate)
            .ToList();
            Console.WriteLine($"After querying appointments. Found {bookedAppointments.Count} appointments.");

            //modification here
            //var availableTimeSlots = allTimeSlots.ToList();


            // // Exclude the booked appointments from the list of all time slots to get available time slots
            // Extract the time slots of the booked appointments
            var bookedTimeSlots = bookedAppointments.Select(a => a.AppointmentTime).ToList();

            // Exclude the booked time slots from the list of all time slots to get available time slots
            var availableTimeSlots = allTimeSlots.Except(bookedTimeSlots).ToList();

            Console.WriteLine("\nAvailable Time Slots:");
            foreach (var timeSlot in availableTimeSlots)
            {
                Console.WriteLine($"Hour: {timeSlot.Hour}, Minute: {timeSlot.Minute}");
            }

            var availableAppointments = availableTimeSlots.Select(timeSlot => new Appointment
            {
                AppointmentDate = appointmentDate,
                AppointmentTime = timeSlot,
                DoctorID = doctorId,
                AppStatus = false
            }).ToList();



            var dto = new AppointmentDTO
            {
                Hospitals = _context.hospitals.ToList(),
                Departments = _context.departments.ToList(),
                Doctors = _context.doctors.ToList(),
                availableAppointments = availableAppointments
            };

            return View(dto);



        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public ActionResult UpdateDetails()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public ActionResult UpdateDetails(Patient model)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine(email + "            <<<<<<<<<<<<<<<<<<<<<<<--------------------------------------------- FOR DEBUG");
            var patient = _context.patients.FirstOrDefault(c => c.Email == email);

            patient.Age = model.Age;
            patient.Height = model.Height;
            patient.Weight = model.Weight;
            patient.Address = model.Address;
            patient.PhoneNumber = model.PhoneNumber;
            _context.SaveChanges();
            return RedirectToAction("Profile", "Patient");
        }
        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var Patient = _context.patients.ToList();
            ApplicationUser patient = _context.patients
            .SingleOrDefault(p => p.ApplicationUser.Email == userEmail);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult AddAppointment(int doctorId, DateTime appointmentDate, DateTime appointmentTime)
        {
            try
            {
                System.Console.WriteLine("DOOOOOCTR" + doctorId);
                System.Console.WriteLine("AAAPDTAE" + appointmentDate.Month);
                System.Console.WriteLine("timeeee" + appointmentTime);


                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
                System.Console.WriteLine("PatientAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA ID" + patient.PatientID);
                System.Console.WriteLine("apppppp iddd" + patient.ApplicationUser.ApplicationUserID);
                if (patient == null)
                {

                    return NotFound("Patient not found.");
                }

                var eagerdoctor = _context.doctors.Include(d => d.ApplicationUser).FirstOrDefault(d => d.DoctorID == doctorId);
                System.Console.WriteLine(" DOOOOOOOC DOOOOC :" + eagerdoctor.DoctorID);
                System.Console.WriteLine(" app app doc doc doc doc doc " + eagerdoctor.ApplicationUser.ApplicationUserID);

                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDate,
                    AppointmentTime = appointmentTime,
                    AppStatus = true,
                    DoctorID = eagerdoctor.ApplicationUser.ApplicationUserID,
                    PatientID = patient.ApplicationUser.ApplicationUserID
                };


                _context.appointments.Add(appointment);
                _context.SaveChanges();

                // Return success response
                return RedirectToAction("SearchAppointment", "Patient");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        public IActionResult ViewDiagnosis()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("DEBUG ViewDiagnosis Patient" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("DEBUG ViewDiagnosis Patient" + patient.ApplicationUserID);
            var diagnoses = _context.diagnoses.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID).ToList();
            foreach (var diag in diagnoses)
            {
                System.Console.WriteLine("DEBUG: Diagnoses List LOg : " + diag.DiagnosisDate);
            }
            return View(diagnoses);
        }
        public IActionResult ViewReport()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("DEBUG ViewReport Patient" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("DEBUG ViewReport Patient" + patient.ApplicationUserID);
            var reports = _context.reports.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID).ToList();
            foreach (var rep in reports)
            {
                System.Console.WriteLine("DEBUG: Diagnoses List LOg : " + rep.filename);
            }
            return View(reports);
        }
        public IActionResult ViewRadiologicalReport()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("DEBUG ViewRR Patient" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("DEBUG ViewRR Patient" + patient.ApplicationUserID);
            var rr = _context.radiologicalReports.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID).ToList();
            foreach (var r in rr)
            {
                System.Console.WriteLine("DEBUG: RR List LOg : " + r.filename);
            }
            return View(rr);
        }
        public IActionResult ViewPrescription()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("DEBUG ViewPrescription Patient" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("DEBUG ViewPrescription Patient" + patient.ApplicationUserID);
            var prescriptions = _context.prescriptions.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID).ToList();
            foreach (var pres in prescriptions)
            {
                System.Console.WriteLine("DEBUG: RR List LOg : " + pres.filename);
            }
            return View(prescriptions);
        }
        public IActionResult ViewInvoice()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            System.Console.WriteLine("DEBUG: ViewInvoice Patient" + userEmail);
            var patient = _context.patients
                .Include(p => p.ApplicationUser)
                .FirstOrDefault(p => p.Email == userEmail);
            System.Console.WriteLine("DEBUG ViewInvoice Patient" + patient.ApplicationUserID);

            var invoices = _context.invoices.Where(p => p.PatientID == patient.ApplicationUser.ApplicationUserID).ToList();
            foreach (var inv in invoices)
            {
                System.Console.WriteLine("DEBUG: Invoice List LOg : " + inv.filename);
            }
            return View(invoices);
        }







    }
}