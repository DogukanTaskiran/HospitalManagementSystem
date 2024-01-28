using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
    public class Patient   : ApplicationUser
    {
        
        public int PatientID {get;set;}
        public int DocID { get;set;}

        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        // References to related classes

        public Doctor Doctor { get;set;}

        public Appointment Appointment { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<Diagnosis> Diagnoses { get; set; }
        public List<Report> Reports { get; set; }
        public List<RadiologicalReport> RadiologicalReports { get; set; }

        public Patient()
        {

        }

    }
}
