using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Prescription : BaseEntity
    {
        public int PrescriptionID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime PrescriptionDate { get; set; }

        public string filename { get; set; }

        public string filepath { get; set; }

        public Doctor Doctor { get; set; }      // relation to class doctor
        public Patient Patient { get; set; }    // relation to class patient

        // constructor with parameters


        // default constructor
        public Prescription()
        {
        }
    }
}
