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
        public IActionResult AddAppointment()
        {
            var hospitals = _context.hospitals.ToList();
            var hospitalSelectList = new SelectList(hospitals, "HospitalID", "HospitalName");
            ViewBag.HospitalList = hospitalSelectList;

            var departments = _context.departments.ToList();
            var departmentSelectList = new SelectList(departments, "DepartmentID", "DepartmentName");
            ViewBag.DepartmentList = departmentSelectList;

            var doctors = _context.doctors.ToList();
            var doctorSelectList = new SelectList(doctors, "DoctorID", "DoctorName");
            ViewBag.DoctorList = doctorSelectList;

            return View();
        }

        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            // var Patient = _context.patients.ToList();
            ApplicationUser patient = _context.patients
            .Include(x => x.Appointments)
            .SingleOrDefault(p => p.ApplicationUser.Email == userEmail);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
    }
}