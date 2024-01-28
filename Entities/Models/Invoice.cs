using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Invoice : BaseEntity
    {
        public int InvoiceID { get; set; }
        public int PatientID { get; set; }
        public string ServiceDescription { get; set; }
        public int InvoicePrice { get; set; }

        public  Patient Patient { get; set; } // reference to class patient

        // constructor with parameters
        public Invoice(int patientID, string serviceDescription, int invoicePrice)
        {
            PatientID = patientID;
            ServiceDescription = serviceDescription;
            InvoicePrice = invoicePrice;
        }
        // default constructor
        public Invoice() { 
        }
    }
}
