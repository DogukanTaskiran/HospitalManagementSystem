using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Patient : ApplicationUser
    {

        public int DoctorID { get; set; } // Forgeign key


        //relaations ebl below

        public Doctor Doctor { get; set; } // patient has one doctor

        public Room Room { get; set; }

        public virtual List<Prescription> Prescriptions { get; set; } //one patient has many prescriptions
        public virtual List<Visit> Visits { get; set; }//one patient has many visits
        public virtual List<Invoice> Invoices { get; set; }//one patient has many invoices

    }
}
