using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Entities.Models
{
    
    public class Doctor : ApplicationUser
   {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorID { get; set; }
        // public int PatientID { get; set; }
        public int DepartmentID { get; set; }
        public int RoomNumber { get; set; }

        public Department Departments { get; set; }  // reference to Department
        public List<Appointment> Appointments { get; set; }

        public List<Prescription> Prescriptions{ get; set; } 
        public List<Diagnosis> Diagnoses{ get; set; }
        // public List<Patient> Patients{ get; set; }

        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        // constructor with parameters
        public Doctor(int departmentID, int roomNumber)
        {
            DepartmentID = departmentID;
            RoomNumber = roomNumber;
        }
        // default constructor
        public Doctor() { 
        } 
    }
}
