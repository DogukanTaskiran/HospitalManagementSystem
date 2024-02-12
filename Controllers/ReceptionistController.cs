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