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
        public IActionResult ViewDiagnosis(int id){
            System.Console.WriteLine("VIEW DIAGNOSIS DEBUG : " + id);

            var diagnosis = _context.diagnoses.Where(d=>d.PatientID==id).ToList();

            return View(diagnosis);
        }

        [HttpGet]
        public IActionResult AddDiagnosis(){

            return View();
        }




    }


}
