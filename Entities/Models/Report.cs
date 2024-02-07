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
        public string ReportDescription { get; set; }

        public string filename {get;set;}

        public string filepath {get;set;}

        public Patient Patient { get; set; }        // Reference to class Patient  
            // Reference to class Diagnosis 


        // Default Constructor 
        public Report()
        {
            
        }

    }
}
