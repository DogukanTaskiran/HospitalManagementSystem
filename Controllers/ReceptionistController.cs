using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    public class ReceptionistController : Controller
    {


        private readonly ApplicationDbContext _context;

        public ReceptionistController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
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
                .Where(d => d.DepartmentID == departmentId && (d.offDuty == null || d.offDuty == false))
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
        // public IActionResult ViewPatient(string searchString) // search belki geliştirilip ayırılınabilir
        // {
        //     var patients = _context.patients.Where(d => d.Role == "Patient").ToList();

        //     if (!string.IsNullOrEmpty(searchString))
        //     {
        //         patients = patients.Where(p =>
        //             p.Email.Contains(searchString)
        //         ).ToList();
        //     }

        //     return View(patients);
        // }
        public IActionResult ViewPatient(string searchString, int? page)
        {
            int pageSize = 3; // 
            int pageNumber = page ?? 1; // If no page is specified, default to page 1

            var patients = _context.patients.Where(d => d.Role == "Patient").ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p =>
                    p.Email.Contains(searchString)
                ).ToList();
            }

            int totalPatients = patients.Count();
            int totalPages = (int)Math.Ceiling((double)totalPatients / pageSize);

            patients = patients.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(patients);
        }


        public IActionResult ViewInvoice(int id)
        {
            var invoices = _context.invoices.Where(r => r.PatientID == id).ToList();
            var DTO = new InvoiceDTO
            {
                Invoices = invoices,
                PatientID = id
            };
            return View(DTO);
        }

        [HttpPost]
        public IActionResult AddInvoice(InvoiceDTO model)
        {
            System.Console.WriteLine("ADD INVOICE DEBUG PATIENT ID : " + model.PatientID);
            System.Console.WriteLine("ADD INVOICE DEBUG DESC : " + model.ServiceDescription);
            System.Console.WriteLine("ADD INVOIE DEBUG FileName: " + model.InvoiceFile.FileName);

            if (ModelState.IsValid)
            {
                // Save the report to the database
                var invoice = new Invoice
                {
                    ServiceDescription = model.ServiceDescription,
                    PatientID = model.PatientID,
                    InvoiceDate = DateTime.Now,
                    InvoicePrice = model.InvoicePrice,
                    filename = model.InvoiceFile.FileName,
                    filepath = UploadFile(model.InvoiceFile) // guid eklenmiş versiyonu
                };

                _context.invoices.Add(invoice);
                _context.SaveChanges();

                return RedirectToAction("ViewInvoice", "Receptionist", new { id = model.PatientID });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddAnonymousPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAnonymousPatient(AnonymousRegisterDTO model)
        {

            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    PhoneNumber = model.PhoneNumber,
                    Age = 0,
                    Height = 0,
                    Weight = 0,
                    Address = "anonymous_not_provided",
                    Gender = "anonymous_not_provided",
                    BloodType = "anonymous_not_provided",
                    Email = "anonymous_not_provided",
                    Password = "anonymous_not_provided",
                    Role = "Patient"
                };
                _context.patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("ViewPatient", "Receptionist");
            }

            return View(model);
        }
        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            var receptionist = _context.receptionists
            .Include(p => p.ApplicationUser)
            .FirstOrDefault(p => p.Email == userEmail);



            if (receptionist == null)
            {
                return NotFound();
            }

            var profileDTO = new ProfileDTO
            {
                Receptionist = receptionist
            };

            return View(profileDTO);

        }
        [Authorize(Roles = "Receptionist")]
        [HttpGet]
        public ActionResult UpdateDetails()
        {
            return View();
        }

        [Authorize(Roles = "Receptionist")]
        [HttpPost]
        public ActionResult UpdateDetails(Receptionist model)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine(email + "            <<<<<<<<<<<<<<<<<<<<<<<--------------------------------------------- FOR DEBUG");
            var receptionist = _context.receptionists.FirstOrDefault(c => c.Email == email);

            if (receptionist != null)
            {
                receptionist.Age = model.Age;
                receptionist.Height = model.Height;
                receptionist.Weight = model.Weight;
                receptionist.Address = model.Address;
                receptionist.PhoneNumber = model.PhoneNumber;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("receptionist NULL DÖNÜYORSA <----------------------------------------------------------------------");
            }

            return RedirectToAction("Profile", "Receptionist");
        }
        [HttpGet]
        public IActionResult SearchAppointment(int id)
        {
            var hospitals = _context.hospitals.ToList();
            var departments = _context.departments.ToList();
            var doctors = _context.doctors.ToList();

            System.Console.WriteLine("DEBUG: RECEPTIONIST SEARCH APPOINTMENT GET :  " + id);



            var dto = new AppointmentDTO
            {
                Hospitals = hospitals,
                Departments = departments,
                Doctors = doctors,
                PatientID = id,
            };

            return View(dto);
        }
        [HttpPost]
        public IActionResult SearchAppointment(int hospitalId, int departmentId, int doctorId, DateTime appointmentDate, int patientId)
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
                Doctors = _context.doctors.Where(d => d.offDuty == false || d.offDuty == null).ToList(),
                availableAppointments = availableAppointments,
                SelectedHospitalId = hospitalId,
                SelectedDepartmentId = departmentId,
                SelectedDoctorId = doctorId,
                SelectedDate = appointmentDate,
                PatientID = patientId
            };
            System.Console.WriteLine("DTO DEPARTMENT ID" + dto.SelectedDepartmentId);
            System.Console.WriteLine("DTO HOSPITAL ID" + dto.SelectedHospitalId);

            return View(dto);



        }
        [HttpPost]
        public IActionResult AddAppointment(int doctorId, DateTime appointmentDate, DateTime appointmentTime , int patientId)
        {
            try
            {
                System.Console.WriteLine("DOOOOOCTR" + doctorId);
                System.Console.WriteLine("AAAPDTAE" + appointmentDate.Month);
                System.Console.WriteLine("timeeee" + appointmentTime);
                System.Console.WriteLine("DEBUG : AddAppointment Receptionist patientId :" + patientId);


                // var userEmail = User.FindFirstValue(ClaimTypes.Name);
                // var patient = _context.patients
                // .Include(p => p.ApplicationUser)
                // .FirstOrDefault(p => p.Email == userEmail);
                // System.Console.WriteLine("PatientAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA ID" + patient.PatientID);
                // System.Console.WriteLine("apppppp iddd" + patient.ApplicationUser.ApplicationUserID);
                // if (patient == null)
                // {

                //     return NotFound("Patient not found.");
                // }

                var eagerdoctor = _context.doctors.Include(d => d.ApplicationUser).FirstOrDefault(d => d.DoctorID == doctorId);
                System.Console.WriteLine("DEBUG: AddAppointment Receptionist eagerdoctor - DoctorID:" + eagerdoctor.DoctorID);
                System.Console.WriteLine("DEBUG: AddAppointment Receptionist eagerdoctor- AppID: " + eagerdoctor.ApplicationUser.ApplicationUserID);

                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDate,
                    AppointmentTime = appointmentTime,
                    AppStatus = true,
                    DoctorID = eagerdoctor.ApplicationUser.ApplicationUserID,
                    PatientID = patientId
                };


                _context.appointments.Add(appointment);
                _context.SaveChanges();

                // Return success response
                return RedirectToAction("SearchAppointment", "Receptionist");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
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

    }

}