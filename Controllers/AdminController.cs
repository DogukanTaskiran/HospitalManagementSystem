using Microsoft.AspNetCore.Mvc;
using Entities.Models;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.Controllers
{
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        //doctor crud
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewPatient(string searchString, int? page)
        {
            int pageSize = 1; // şimdilik 1 kalsın daha fazla patient ekleyene kadar
            int pageNumber = page ?? 1; // If no page is specified, default to page 1

            var patients = _context.patients.Where(d => d.Role == "Patient").ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p =>
                    p.Email.Contains(searchString)
                ).ToList();
            }

            int totalPatients = patients.Count();
            int totalPages = (int)Math.Ceiling((double)totalPatients / pageSize);

            patients = patients.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNumber;

            return View(patients);
        }

        [HttpGet]
        public IActionResult GetDepartments(int hospitalID)
        {
            var departments = _context.departments.Where(d => d.HospitalID == hospitalID).ToList();
            return Json(departments);
        }

        [HttpGet]
        public IActionResult AddDoctor(int id)
        {
            // //js için gerekliydi
            // ViewBag.Hospitals = _context.hospitals.ToList();

            // ViewBag.Departments = _context.departments.ToList();
            var doc = new DoctorDTO
            {
                DepartmentID = id
            };

            return View(doc);
        }

        [HttpPost]
        public IActionResult AddDoctor(DoctorDTO model)
        {
            Console.WriteLine("DepartmentID: " + model.DepartmentID);
            if (ModelState.IsValid)
            {
                if (_context.applicationUsers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }
                var doctor = new Doctor
                {
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
                    Role = "Doctor"
                };
                _context.doctors.Add(doctor);
                _context.SaveChanges();

                return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID });
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewDoctor()
        {

            var doctorsList = _context.doctors.ToList();//dto eklenebilir

            foreach (var doctor in doctorsList)
            {
                Console.WriteLine($"Doctor Name: {doctor.Name}, ApplicationUserID: {doctor.ApplicationUserID}");
            }
            return View(doctorsList);

        }
        [HttpPost]
        public IActionResult DeleteDoctor(string email)
        {
            System.Console.WriteLine("idAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + email);
            var doctor = _context.doctors.FirstOrDefault(d => d.Email == email);

            doctor.DeletedDate = DateTime.Now;
            doctor.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = doctor.DepartmentID });
        }
        public IActionResult UpdateDoctor()
        {
            return View();
        }
        //hospital crud

        public IActionResult AddHospital()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddHospital(HospitalDTO model)
        {
            if (ModelState.IsValid)
            {
                if (_context.hospitals.Any(u => u.HospitalName == model.HospitalName))
                {
                    ModelState.AddModelError(string.Empty, "Hospital already exists.");
                    return View(model);
                }
                var hospital = new Entities.Models.Hospital
                {
                    HospitalName = model.HospitalName,
                    Address = model.Address,
                    PhoneNum = model.PhoneNum
                };

                _context.hospitals.Add(hospital);
                _context.SaveChanges();

                return RedirectToAction("ViewHospital");
            }
            return View(model);
        }


        public IActionResult ViewHospital()
        {
            var hospitalList = _context.hospitals.Where(u => u.Status != Entities.Enums.DataStatus.Deleted).ToList(); //dto eklenebilir
            return View(hospitalList);
        }

        public IActionResult UpdateHospital()
        {
            return View();
        }

        public IActionResult DeleteHospital(int id)
        {
            var hospital = _context.hospitals.FirstOrDefault(u => u.HospitalID == id);
            System.Console.WriteLine("idAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + id);
            hospital.DeletedDate = DateTime.Now;
            hospital.Status = Entities.Enums.DataStatus.Deleted;

            _context.SaveChanges();

            return RedirectToAction("ViewHospital", "Admin");
        }




        //department crud
        [HttpGet]
        public IActionResult ViewDepartment(int id)
        {

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

        public IActionResult AddDepartment(int id)
        {

            var hospitalName = _context.hospitals.First(u => u.HospitalID == id);

            var model = new DepartmentDTO
            {
                HospitalID = id,
                HospitalName = hospitalName.HospitalName
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDepartment(DepartmentDTO model)
        {
            if (ModelState.IsValid)
            {
                if (_context.departments.Any(u => u.DepartmentName == model.DepartmentName && u.HospitalID == model.HospitalID))
                {
                    ModelState.AddModelError(string.Empty, "Department already exists");
                    return View(model);
                }
                var Department = new Department
                {

                    DepartmentName = model.DepartmentName,
                    HospitalID = model.HospitalID


                };
                _context.departments.Add(Department);
                _context.SaveChanges();
                return RedirectToAction("ViewDepartment", new { id = model.HospitalID }); // RouteValue newleniyor dikkat

            }

            return View(model);
        }
        public IActionResult DeleteDepartment(int id)
        {
            var department = _context.departments.FirstOrDefault(d => d.DepartmentID == id);
            department.DeletedDate = DateTime.Now;
            department.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewDepartment", new { id = department.HospitalID });
        }

        [HttpGet("Admin/ViewPersonnel/{id}")]
        public IActionResult ViewPersonnel(int id)
        {
            var doctors = _context.doctors.Where(d => d.DepartmentID == id && d.Status != Entities.Enums.DataStatus.Deleted).ToList();
            var nurses = _context.nurses.Where(d => d.DepartmentID == id && d.Status != Entities.Enums.DataStatus.Deleted).ToList();
            var receptionists = _context.receptionists.Where(d => d.DepartmentID == id && d.Status != Entities.Enums.DataStatus.Deleted).ToList();

            var personnel = new PersonnelDTO
            {
                DepartmentID = id,
                Nurses = nurses,
                Doctors = doctors,
                Receptionists = receptionists
            };

            return View(personnel);
        }

        //nurse crud
        [HttpGet]
        public IActionResult AddNurse(int id)
        {
            var model = new NurseDTO
            {
                DepartmentID = id
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddNurse(NurseDTO model)
        {
            Console.WriteLine("DepartmentID: " + model.DepartmentID);
            if (ModelState.IsValid)
            {
                if (_context.applicationUsers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }
                var nurse = new Nurse
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    DepartmentID = model.DepartmentID,
                    Role = "Nurse"
                };
                _context.nurses.Add(nurse);
                _context.SaveChanges();
                return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteNurse(string email)
        {

            System.Console.WriteLine("DEBUG : DELETE NURSE EMAIL :" + email);
            var nurse = _context.nurses.FirstOrDefault(n => n.Email == email);

            nurse.DeletedDate = DateTime.Now;
            nurse.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = nurse.DepartmentID });
        }


        public IActionResult ViewAdmin()
        {
            var admins = _context.admins.Where(u => u.Role == "Admin" && u.Status != Entities.Enums.DataStatus.Deleted).ToList();
            return View(admins);
        }
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAdmin(AdminDTO model)
        {
            Console.WriteLine("Admin Name: " + model.Name);
            if (ModelState.IsValid)
            {
                if (_context.applicationUsers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }
                var admin = new Admin
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    Role = "Admin"
                };
                _context.admins.Add(admin);
                _context.SaveChanges();

                return RedirectToAction("ViewAdmin", "Admin");
            }
            return View(model);

        }
        public IActionResult DeleteAdmin(string email)
        {
            var admin = _context.admins.FirstOrDefault(d => d.Email == email);
            admin.DeletedDate = DateTime.Now;
            admin.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewAdmin", "Admin");
        }

        [HttpGet]
        public IActionResult AddReceptionist(int id)
        {
            var recep = new ReceptionistDTO
            {
                DepartmentID = id
            };

            return View(recep);
        }

        [HttpPost]
        public IActionResult AddReceptionist(ReceptionistDTO model)
        {
            Console.WriteLine("DEBUG: ADD RECEPTIONIST DepartmentID: " + model.DepartmentID);
            if (ModelState.IsValid)
            {
                if (_context.applicationUsers.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email is already registered.");
                    return View(model);
                }
                var receptionist = new Receptionist
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name,
                    Surname = model.Surname,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    BloodType = model.BloodType,
                    Gender = model.Gender,
                    DepartmentID = model.DepartmentID,
                    Role = "Receptionist"
                };
                _context.receptionists.Add(receptionist);
                _context.SaveChanges();

                return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID });
            }
            return View(model);
        }

        [HttpPost]

        public IActionResult DeleteReceptionist(string email)
        {
            var recep = _context.receptionists.FirstOrDefault(r => r.Email == email);
            recep.DeletedDate = DateTime.Now;
            recep.Status = Entities.Enums.DataStatus.Deleted;
            _context.SaveChanges();
            return RedirectToAction("ViewPersonnel", new { id = recep.DepartmentID });
        }

        [HttpGet]
        public IActionResult UpdateReceptionist(int id, int departmentId)
        {
            //bu ve diğer roller için önceden var olan modeli placeholder olarak viewe koymak iyi olabilir
            var dto = new ReceptionistDTO
            {
                ApplicationUserID = id,
                DepartmentID = departmentId
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult UpdateReceptionist(ReceptionistDTO model)
        {
            System.Console.WriteLine("DEBUG: Update Recep POST:" + model.ApplicationUserID);
            var recep = _context.receptionists.FirstOrDefault(r => r.ApplicationUserID == model.ApplicationUserID);

            recep.Name = model.Name;
            recep.Surname = model.Name;
            recep.PhoneNumber = model.PhoneNumber;
            recep.Address = model.Address;
            recep.Gender = model.Gender;
            recep.BloodType = model.BloodType;
            recep.Email = model.Email;
            recep.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID }); ;
        }


        [HttpGet]
        public IActionResult UpdateDoctor(int id, int departmentId)
        {
            //bu ve diğer roller için önceden var olan modeli placeholder olarak viewe koymak iyi olabilir
            var dto = new DoctorDTO
            {
                ApplicationUserID = id,
                DepartmentID = departmentId
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult UpdateDoctor(DoctorDTO model)
        {
            System.Console.WriteLine("DEBUG: Update DOCTOR POST:" + model.ApplicationUserID);
            var doctor = _context.doctors.FirstOrDefault(r => r.ApplicationUserID == model.ApplicationUserID);

            doctor.RoomNumber = model.RoomNumber;
            doctor.Name = model.Name;
            doctor.Surname = model.Name;
            doctor.PhoneNumber = model.PhoneNumber;
            doctor.Address = model.Address;
            doctor.Gender = model.Gender;
            doctor.BloodType = model.BloodType;
            doctor.Email = model.Email;
            doctor.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID }); ;
        }
        [HttpGet]
        public IActionResult UpdateNurse(int id, int departmentId)
        {
            //bu ve diğer roller için önceden var olan modeli placeholder olarak viewe koymak iyi olabilir
            var dto = new NurseDTO
            {
                ApplicationUserID = id,
                DepartmentID = departmentId
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult UpdateNurse(DoctorDTO model)
        {
            System.Console.WriteLine("DEBUG: Update NURSE POST:" + model.ApplicationUserID);
            var nurse = _context.nurses.FirstOrDefault(r => r.ApplicationUserID == model.ApplicationUserID);


            nurse.Name = model.Name;
            nurse.Surname = model.Name;
            nurse.PhoneNumber = model.PhoneNumber;
            nurse.Address = model.Address;
            nurse.Gender = model.Gender;
            nurse.BloodType = model.BloodType;
            nurse.Email = model.Email;
            nurse.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("ViewPersonnel", new { id = model.DepartmentID }); ;
        }
        public IActionResult UpdateAdmin(int id, int departmentId)
        {
            //bu ve diğer roller için önceden var olan modeli placeholder olarak viewe koymak iyi olabilir
            var dto = new AdminDTO
            {
                ApplicationUserID = id,
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult UpdateAdmin(DoctorDTO model)
        {
            System.Console.WriteLine("DEBUG: Update ADMIN POST:" + model.ApplicationUserID);
            var admin = _context.admins.FirstOrDefault(r => r.ApplicationUserID == model.ApplicationUserID);


            admin.Name = model.Name;
            admin.Surname = model.Name;
            admin.PhoneNumber = model.PhoneNumber;
            admin.Address = model.Address;
            admin.Gender = model.Gender;
            admin.BloodType = model.BloodType;
            admin.Email = model.Email;
            admin.Password = model.Password;

            _context.SaveChanges();

            return RedirectToAction("ViewAdmin", "Admin"); ;
        }



    }
}