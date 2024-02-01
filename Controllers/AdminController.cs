using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hospital.Controllers{
    //[Authorize(Policy ="AdminPolicy")]
    public class AdminController : Controller{

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext applicationDbContext){
            _context = applicationDbContext;
        }
        //doctor crud
        public IActionResult Index(){
            return View();
        }
        

       [HttpGet]
        public IActionResult GetDepartments(int hospitalID)
        {
            var departments = _context.departments.Where(d => d.HospitalID == hospitalID).ToList();
            return Json(departments);
        }
        public IActionResult AddDoctor(){
            ViewBag.Hospitals = _context.hospitals.ToList();
            
            ViewBag.Departments = _context.departments.ToList();

            return View(new DoctorDTO());
        }

        [HttpPost]
        public IActionResult AddDoctor(DoctorDTO model){
            if(ModelState.IsValid){
                if(_context.applicationUsers.Any(u=>u.Email==model.Email)){
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);                    
                }
                var doctor =  new Doctor{
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    DepartmentID = model.SelectedDepartmentID,
                    RoomNumber = model.RoomNumber,
                    Role ="Doctor"
                };
                _context.doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("ViewDoctor" , "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewDoctor(){
            var doctorsList = _context.doctors.ToList(); //dto eklenebilir
            return View(doctorsList);

        }
        public IActionResult DeleteDoctor(){
            return View();
        }
        public IActionResult UpdateDoctor(){
            return View();
        }
        //hospital crud

        public IActionResult AddHospital(){
            return View();
        }
        [HttpPost]
        public IActionResult AddHospital(HospitalDTO model){
            if(ModelState.IsValid){
                if(_context.hospitals.Any(u=>u.HospitalName == model.HospitalName)){
                    ModelState.AddModelError(string.Empty, "Hospital already exists.");
                    return View(model);                    
                }
                var hospital=  new Entities.Models.Hospital{
                    HospitalName= model.HospitalName,
                    Address = model.Address,
                    PhoneNum = model.PhoneNum
                };

                _context.hospitals.Add(hospital);
                _context.SaveChanges();

                return RedirectToAction("ViewHospital");
            }
            return View(model);
        }


        public IActionResult ViewHospital(){
            var hospitalList = _context.hospitals.ToList(); //dto eklenebilir
            return View(hospitalList); 
        }

        public IActionResult UpdateHospital(){
            return View();
        }

        public IActionResult DeleteHospital(){
            return View();
        }




        //department crud
        [HttpGet]
        public IActionResult ViewDepartment(int id){

            var departmentList = _context.departments.Where(u => u.HospitalID == id).ToList();
            var hospitalName = _context.hospitals.FirstOrDefault(h => h.HospitalID == id)?.HospitalName;

            var departmentDTO = new DepartmentDTO
            {
                Departments = departmentList,
                HospitalName = hospitalName,
                HospitalID = id
            };

            return View(departmentDTO);
        }

        public IActionResult AddDepartment(int id){

            var hospitalName = _context.hospitals.First(u=>u.HospitalID==id);

            var model = new DepartmentDTO
            {
                HospitalID = id,
                HospitalName =  hospitalName.HospitalName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDepartment(DepartmentDTO model){
            if(ModelState.IsValid){
                if(_context.departments.Any(u=>u.DepartmentName == model.DepartmentName && u.HospitalID == model.HospitalID)){
                    ModelState.AddModelError(string.Empty , "Department already exists");
                    return View(model);
                }
                var Department = new Department{
                    
                    DepartmentName=model.DepartmentName,
                    HospitalID = model.HospitalID
                    

                };
                _context.departments.Add(Department);
                _context.SaveChanges();
                return RedirectToAction("ViewDepartment", new { id = model.HospitalID }); // RouteValue newleniyor dikkat
                
            }

            return View(model);
        }

        //nurse crud

        //receptionist crud


    }
}