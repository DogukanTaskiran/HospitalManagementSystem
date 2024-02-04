using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers{

    public class DoctorController : Controller{

        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext applicationDbContext){
            _context =applicationDbContext;
        }

        public IActionResult Index(){
            return View();
        }





    }


}
