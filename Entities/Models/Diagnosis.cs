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
        public int DocID { get; set; }
        public int PatientID { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiagnosisDescription { get; set; }
        public Doctor Doctor { get; set; }      // reference to class doctor
        public Patient Patient { get; set; }    // reference to class patient
        public Report Reports{ get; set; }    // reference to class patient

        // constructor with parameters
        public Diagnosis(int docID, int patientID, DateTime diagnosisDate, string diagnosisDescription)
        {
            DocID = docID;
            PatientID = patientID;
            DiagnosisDate = diagnosisDate;
            DiagnosisDescription = diagnosisDescription;
        }
        // default constructor
        public Diagnosis()
        {

        }
    }
}
