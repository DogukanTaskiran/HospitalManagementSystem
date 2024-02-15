using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Hospital.Controllers
{
    public class NurseController : Controller
    {

        private readonly ApplicationDbContext _context;

        public NurseController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [Authorize(Roles = "Nurse")]
        [HttpGet]
        public ActionResult UpdateDetails()
        {
            return View();
        }

        [Authorize(Roles = "Nurse")]
        [HttpPost]
        public ActionResult UpdateDetails(Nurse model)
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            Console.WriteLine(email + "            <<<<<<<<<<<<<<<<<<<<<<<--------------------------------------------- FOR DEBUG");
            var Nurse = _context.nurses.FirstOrDefault(c => c.Email == email);
            if (Nurse != null)
            {
                Nurse.Age = model.Age;
                Nurse.Height = model.Height;
                Nurse.Weight = model.Weight;
                Nurse.Address = model.Address;
                Nurse.PhoneNumber = model.PhoneNumber;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Nurse NULL DÖNÜYORSA <----------------------------------------------------------------------");
            }

            return RedirectToAction("Profile", "Nurse");
        }
        public IActionResult Profile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);

            var Nurse = _context.nurses
            .Include(p => p.ApplicationUser)
            .FirstOrDefault(p => p.Email == userEmail);



            if (Nurse == null)
            {
                return NotFound();
            }

            var profileDTO = new ProfileDTO
            {
                Nurse = Nurse
            };

            return View(profileDTO);

        }

    }
}
