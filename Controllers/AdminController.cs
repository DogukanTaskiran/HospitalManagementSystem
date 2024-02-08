using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers{
    [Authorize(Policy ="AdminPolicy")]
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

        [HttpGet]
        public IActionResult AddDoctor(int id){
            // //js için gerekliydi
            // ViewBag.Hospitals = _context.hospitals.ToList();
            
            // ViewBag.Departments = _context.departments.ToList();
            var doc = new DoctorDTO{
                DepartmentID = id
            };

            return View(doc);
        }

        [HttpPost]
        public IActionResult AddDoctor(DoctorDTO model){
            Console.WriteLine("DepartmentID: " + model.DepartmentID);
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
                    DepartmentID = model.DepartmentID,
                    RoomNumber = model.RoomNumber,
                    Role ="Doctor"
                };
                _context.doctors.Add(doctor);
                _context.SaveChanges();
                
                 return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID});
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewDoctor(){

            var doctorsList = _context.doctors.ToList();//dto eklenebilir
            
            foreach (var doctor in doctorsList)
            {
                    Console.WriteLine($"Doctor Name: {doctor.Name}, ApplicationUserID: {doctor.ApplicationUserID}");
            }
            return View(doctorsList);

        }
        [HttpPost]
        public IActionResult DeleteDoctor(string email){
            System.Console.WriteLine("idAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + email);
            var doctor = _context.doctors.FirstOrDefault(d=>d.Email==email);
            
            doctor.DeletedDate = DateTime.Now;
            doctor.Status=Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();

             return RedirectToAction("ViewPersonnel", new { id = doctor.DepartmentID});
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
            var hospitalList = _context.hospitals.Where(u=>u.Status != Entities.Enums.DataStatus.Deleted).ToList(); //dto eklenebilir
            return View(hospitalList); 
        }

        public IActionResult UpdateHospital(){
            return View();
        }

        public IActionResult DeleteHospital(int id){
            var hospital = _context.hospitals.FirstOrDefault(u=>u.HospitalID==id);
            System.Console.WriteLine("idAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + id);
            hospital.DeletedDate = DateTime.Now;
            hospital.Status=Entities.Enums.DataStatus.Deleted;

            _context.SaveChanges();

            return RedirectToAction("ViewHospital", "Admin");
        }




        //department crud
        [HttpGet]
        public IActionResult ViewDepartment(int id){

            var departmentList = _context.departments.Where(u => u.HospitalID == id && u.Status != Entities.Enums.DataStatus.Deleted).ToList();
            var hospitalName = _context.hospitals.FirstOrDefault(h => h.HospitalID == id)?.HospitalName;
            

            var departmentDTO = new DepartmentDTO
            {
                Departments = departmentList,
                HospitalName = hospitalName,
                HospitalID = id,
                
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
        public IActionResult DeleteDepartment(int id){
            var department = _context.departments.FirstOrDefault(d=>d.DepartmentID ==id);
            department.DeletedDate = DateTime.Now;
            department.Status=Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewDepartment" ,new { id = department.HospitalID } );
        }

        [HttpGet("Admin/ViewPersonnel/{id}")]
        public IActionResult ViewPersonnel(int id){
            var doctors = _context.doctors.Where(d=>d.DepartmentID ==id && d.Status!=Entities.Enums.DataStatus.Deleted).ToList();
            var nurses = _context.nurses.Where(d=>d.DepartmentID ==id && d.Status!=Entities.Enums.DataStatus.Deleted).ToList();

            var personnel = new PersonnelDTO{
                DepartmentID = id,
                Nurses = nurses,
                Doctors = doctors
            };

            return View(personnel);
        }

        //nurse crud
        [HttpGet]
        public IActionResult AddNurse(int id){
            var model = new NurseDTO{
                DepartmentID = id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddNurse(NurseDTO model){
            Console.WriteLine("DepartmentID: " + model.DepartmentID);
            if(ModelState.IsValid){
                if(_context.applicationUsers.Any(u=>u.Email==model.Email)){
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);                    
                }
                var nurse =  new Nurse{
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    DepartmentID = model.DepartmentID,
                    Role ="Nurse"
                };
                _context.nurses.Add(nurse);
                _context.SaveChanges();
                 return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID});
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteNurse(string email){

            System.Console.WriteLine("DEBUG : DELETE NURSE EMAIL :" + email);
            var nurse = _context.nurses.FirstOrDefault(n=>n.Email ==email);

            nurse.DeletedDate = DateTime.Now;
            nurse.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = nurse.DepartmentID});
        }

        //receptionist crud

        public IActionResult ViewAdmin(){
            var admins = _context.admins.Where(u=>u.Role=="Admin" && u.Status!=Entities.Enums.DataStatus.Deleted).ToList();
            return View(admins);
        }
        [HttpGet]
        public IActionResult AddAdmin(){
            return View();
        }
        [HttpPost]
        public IActionResult AddAdmin(AdminDTO model){
            Console.WriteLine("Admin Name: " + model.Name);
            if(ModelState.IsValid){
                if(_context.applicationUsers.Any(u=>u.Email==model.Email)){
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);                    
                }
                var admin =  new Admin{
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    Role ="Admin"
                };
                _context.admins.Add(admin);
                _context.SaveChanges();
                
                 return RedirectToAction("ViewAdmin", "Admin");
            }
            return View(model);

        }
        public IActionResult DeleteAdmin(string email){
            var admin = _context.admins.FirstOrDefault(d=>d.Email == email);
            admin.DeletedDate = DateTime.Now;
            admin.Status= Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewAdmin", "Admin");
        }

    }
}