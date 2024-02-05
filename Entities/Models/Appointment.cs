using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        
        
        public DateTime AppointmentDate { get; set; }
        public int AppointmentTime { get; set; }
        public bool Status { get; set; }

        
        public Doctor Doctor { get; set; }
        public int? DoctorID { get; set; }          // reference to doctor 
        public Patient Patient { get; set; } 
        public int? PatientID { get; set; }       // reference to patient

        // constructor with parameters

        // default constructor
        public Appointment()
        {
            AppointmentDate=DateTime.Now;
            //appointment time ne olacak?
        }
    }
}
