using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Prescription : BaseEntity
    {
        public int PrescriptionID { get; set; }
        public DateTime PrescriptionDate { get; set; }

        public decimal PrescriptionPrice { get; set; }

        public int PatientID { get; set; }
        //relations below
        public virtual Patient Patient { get; set; }
    }
}
