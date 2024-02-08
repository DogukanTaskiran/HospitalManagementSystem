using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult ViewPatient(string searchString) // search belki geliştirilip ayırılınabilir
        {
            var patients = _context.patients.Where(d => d.Role == "Patient").ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p =>
                    p.Email.Contains(searchString)
                ).ToList();
            }

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