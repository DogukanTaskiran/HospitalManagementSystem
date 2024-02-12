using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult DepartmentList()
        {
            var departments = _context.departments.Include(d => d.Hospital).ToList();
            return View(departments);
        }
    }
}
