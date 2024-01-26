using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
    public class Patient   : ApplicationUser
    {
        
        
        public int PatientID {get;set;}
        //relaations ebl below

        public int DoctorID { get; set; } // Forgeign key
        public Doctor Doctor { get; set; } // patient has one doctor



        public ApplicationUser ApplicationUser{get;set;}
        public int ApplicationUserID{get;set;}

        

        public Room Room { get; set; } //room ile one to one

        public virtual List<Prescription> Prescriptions { get; set; } //one patient has many prescriptions
        public virtual List<Visit> Visits { get; set; }//one patient has many visits
        public virtual List<Invoice> Invoices { get; set; }//one patient has many invoices

    }
}
