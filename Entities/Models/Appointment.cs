using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;

namespace Entities.Models
{
    public class Appointment : BaseEntity
    {
        public int AppointmentID { get; set; }


        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool AppStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DataStatus Status { get; set; }


        public Doctor Doctor { get; set; }
        public int? DoctorID { get; set; }          // reference to doctor 
        public Patient Patient { get; set; }
        public int? PatientID { get; set; }       // reference to patient

        // constructor with parameters

        // default constructor
        public Appointment()
        {

        }
    }
}
