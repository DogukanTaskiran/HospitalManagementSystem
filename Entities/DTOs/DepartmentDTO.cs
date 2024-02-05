using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs{
    public class DepartmentDTO{
        public List<Department>? Departments { get; set; } // View Department
        public string? HospitalName { get; set; } // View Department

        public int HospitalID {get; set;}
        public int DepartmentID {get; set;}

        [Required(ErrorMessage = "Department Name is required")]
        [MaxLength(100, ErrorMessage = "Department name cannot be longer than 100 characters")]
        public string DepartmentName {get;set;}
    }
}
