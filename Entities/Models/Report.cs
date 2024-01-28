using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int PatientID { get; set; }
        public int DiagnosisID { get; set; }
        public string ReportDescription { get; set; }

        public Patient Patient { get; set; }        // Reference to class Patient  
        public Diagnosis Diagnosis { get; set; }     // Reference to class Diagnosis 

        // Constructor with parameters
        public Report(int patientID, int diagnosisID, string reportDescription)
        {
            PatientID = patientID;
            DiagnosisID = diagnosisID;
            ReportDescription = reportDescription;
        }
        // Default Constructor 
        public Report()
        {
            
        }

    }
}
