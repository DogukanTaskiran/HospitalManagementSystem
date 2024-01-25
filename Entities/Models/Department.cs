using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department : BaseEntity
    {
        //public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string PhoneNumber { get; set; }

        public int HospitalID { get; set; }

        //relations below

        public virtual List<Doctor> Doctors { get; set; } // One department has many doctors
        public virtual Hospital Hospital { get; set; }

    }
}
