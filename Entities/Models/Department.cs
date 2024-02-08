using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department : BaseEntity
    {
       public int DepartmentID { get; set; }
       public string DepartmentName { get; set; }
       public int HospitalID { get; set; }
       
       public Hospital Hospital { get; set; }   // Reference to hospital
       public List<Doctor> Doctors{ get; set; }   // Reference to doctor
       public List<Nurse> Nurses { get; set; } // Reference to nurse

       public List<Receptionist> Receptionists {get;set;} // reference to receptionist
       
       
       // constructor with parameters

       // default constructor
       public Department() { 
       
       
       }  
    }
}
