using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using System.Linq;

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
            var departments = _context.departments.ToList(); // 'departments' küçük harfle başlıyor
            return View(departments);
        }
    }
}
