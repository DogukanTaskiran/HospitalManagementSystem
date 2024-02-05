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

            var appointments = _context.appointments
                .Where(a => a.DoctorID == doctorId && a.AppointmentDate.Date == appointmentDate.Date)
                .ToList();
                
            Console.WriteLine("Number of appointments retrieved: " + appointments.Count);

            if (appointments.Count == 0)
            {
                appointments = new List<Appointment>();
                for (int i = 0; i < 9; i++)
                {
                    var appointment = new Appointment
                    {
                        AppointmentDate = appointmentDate,
                        AppointmentTime = i,
                        DoctorID = doctorId,
                        Status = false,

                    };
                    appointments.Add(appointment);
                }


            }
            var dto = new AppointmentDTO
            {
                Hospitals = _context.hospitals.ToList(),
                Departments = _context.departments.ToList(),
                Doctors = _context.doctors.ToList(),
                availableAppointments = appointments
            };

            return View(dto);



        }

        [HttpPost]
        public IActionResult AddAppointment(int doctorId, DateTime appointmentDate, int appointmentTime)
        {
            try
            {
                System.Console.WriteLine("DOOOOOCTR" + doctorId);
                System.Console.WriteLine("AAAPDTAE" + appointmentDate.Month);
                System.Console.WriteLine("timeeee" + appointmentTime);

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                var patient = _context.patients.FirstOrDefault(p => p.Email == userEmail);
                System.Console.WriteLine("PatientAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA ID" + patient.PatientID);
                if (patient == null)
                {

                    return NotFound("Patient not found.");
                }


                var appointment = new Appointment
                {
                    AppointmentDate = appointmentDate,
                    AppointmentTime = appointmentTime,
                    Status = false, 
                    DoctorID = doctorId,
                    PatientID = patient.PatientID, 
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