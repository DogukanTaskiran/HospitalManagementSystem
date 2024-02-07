using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Diagnosis
    {
        public int DiagnosisID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiagnosisDescription { get; set; }
        public Doctor Doctor { get; set; }      // reference to class doctor
        public Patient Patient { get; set; }    // reference to class patient
        

        // constructor with parameters

        // default constructor

        public Diagnosis(){
            DiagnosisDate = DateTime.Now;
        }

    }
}
