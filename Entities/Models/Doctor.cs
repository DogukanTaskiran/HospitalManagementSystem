using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
    public class Doctor
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Gender { get; set; }

        public string BloodType {get; set;}
        public int DoctorID {get;set;}
        
        
        
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; } //


        public int ApplicationUserID{get;set;}
        public ApplicationUser ApplicationUser {get; set;}


        //relations below
        
        

        public virtual  List<Patient> Patients { get; set; }// One doctor has many patients
        public virtual  List<Visit> Visits { get; set;}// One doctor has many visits

         



    }
}
