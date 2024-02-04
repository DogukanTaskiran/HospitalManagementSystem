using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    
    public class Patient   : ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientID {get;set;}
        
        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // References to related classes

        // public Doctor Doctor { get;set;}
        // public int DoctorID { get;set;}

        public List<Appointment> Appointments { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Diagnosis> Diagnoses { get; set; }
        public List<Report> Reports { get; set; }
        public List<RadiologicalReport> RadiologicalReports { get; set; }

        

    }
}
