using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RadiologicalReport
    {
        public int RadiologicalReportID { get; set; }
        public int PatientID { get; set; }
        public string RrDescription { get; set; }
        public string filename { get; set; }

        public string filepath { get; set; }

        public Patient Patient { get; set; } // reference to class patient



        // default constructor
        public RadiologicalReport()
        {

        }
    }
}
