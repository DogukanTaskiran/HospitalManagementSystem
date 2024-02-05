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
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PatientController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult Index()
        {

            return View();
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

            var bookedAppointments = _context.appointments.Where(a=>a.DoctorID == appid && a.AppointmentDate == appointmentDate)
            .ToList(); 
            Console.WriteLine($"After querying appointments. Found {bookedAppointments.Count} appointments.");

            //modification here
            var availableTimeSlots = allTimeSlots.ToList();

            Console.WriteLine("\nAvailable Time Slots:");
            foreach (var timeSlot in availableTimeSlots)
            {
                Console.WriteLine($"Hour: {timeSlot.Hour}, Minute: {timeSlot.Minute}");
            }
            // // Exclude the booked appointments from the list of all time slots to get available time slots
            

            var availableAppointments = availableTimeSlots.Select(timeSlot => new Appointment
            {
                AppointmentDate = appointmentDate,
                AppointmentTime = timeSlot,
                DoctorID = doctorId,
                Status = false
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
                    Status = true,
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



























        // public IActionResult AddAppointment()
        // {
        //     var hospitals = _context.hospitals.ToList();
        //     var hospitalSelectList = new SelectList(hospitals, "HospitalID", "HospitalName");
        //     ViewBag.HospitalList = hospitalSelectList;

        //     var departments = _context.departments.ToList();
        //     var departmentSelectList = new SelectList(departments, "DepartmentID", "DepartmentName");
        //     ViewBag.DepartmentList = departmentSelectList;

        //     var doctors = _context.doctors.ToList();
        //     var doctorSelectList = new SelectList(doctors, "DoctorID", "DoctorName");
        //     ViewBag.DoctorList = doctorSelectList;

        //     return View();
        // }

        // public IActionResult Profile()
        // {
        //     var userEmail = User.FindFirstValue(ClaimTypes.Name);
        //     // var Patient = _context.patients.ToList();
        //     ApplicationUser patient = _context.patients
        //     .Include(x => x.Appointments)
        //     .SingleOrDefault(p => p.ApplicationUser.Email == userEmail);

        //     if (patient == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(patient);
        // }
    }
}