using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers;


public class HomeController : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
