using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;

namespace Hospital.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HospitalList()
        {

            var hospitals = _context.hospitals.ToList();


            return View(hospitals);
        }
    }
}
