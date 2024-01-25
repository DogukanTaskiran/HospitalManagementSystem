using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Hospital : BaseEntity
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public string PhoneNumber  { get; set; }

        //relations below
        
        public virtual List<Department> Departments { get; set; } // one hospital has many departments

    }
}
