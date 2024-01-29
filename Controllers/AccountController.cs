using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;

namespace Hospital.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext applicationDbContext){
        _context = applicationDbContext;
    } 
    

    public IActionResult Register()
    {
        
        return View();
    }

    [HttpPost]
    public IActionResult Register(ApplicationUserDTO model)
    {
        if(ModelState.IsValid){
            if(_context.applicationUsers.Any(u=>u.Email==model.Email)){
                ModelState.AddModelError(string.Empty, "Email is already registered.");
                return View(model);
            } 
            var patient = new Patient
            {
                Email = model.Email,
                Password = model.Password,
                Name = model.Name,
                Surname = model.Surname,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                BloodType = model.BloodType,
                Gender = model.Gender,
                
            };
            patient.PatientID = patient.ApplicationUserID;

            _context.patients.Add(patient);
            _context.SaveChanges();
            
            return RedirectToAction("Login");
        }
        return View(model);
    }

    public IActionResult Login(){
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginDTO model){
        if(ModelState.IsValid){
            var user = _context.applicationUsers.SingleOrDefault(u=>u.Email == model.Email && u.Password ==model.Password);
            if(user!=null){
                return RedirectToAction("Index","Home");
            }
            else{
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return View(model); // something is wronga
    }



}
