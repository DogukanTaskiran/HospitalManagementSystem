using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Invoice : BaseEntity
    {
        public int InvoiceID { get; set; }
        public string ServiceDescription { get; set; }
        public decimal InvoicePrice { get; set; }

        

        //relations below

        public virtual Patient Patient { get; set; }
        public int PatientID { get; set; }

        
    }
}
