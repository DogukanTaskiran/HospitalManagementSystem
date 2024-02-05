using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Entities.Models;
using Entities.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

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
                Age = model.Age,
                Weight = model.Weight,
                Height = model.Height,
                Surname = model.Surname,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                BloodType = model.BloodType,
                Gender = model.Gender,
                Role ="Patient"
                
            };
            patient.PatientID = patient.ApplicationUserID; // ?

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
                var Role = user.Role;
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Sid, user.ApplicationUserID.ToString()),
                    new Claim(ClaimTypes.Name , user.Email),
                    new Claim(ClaimTypes.Role , user.Role)
                };
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync( // yukardaki claimlerlebir cookie oluşturma
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity)
                    ); // property set edilmedi
                return RedirectToAction("Index","Home");
            }
            else{
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
        }
        return View(model);
    }

    public IActionResult Logout(){
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index","Home");
    }

    public IActionResult AccessDenied(){
        return View();
    }


}
