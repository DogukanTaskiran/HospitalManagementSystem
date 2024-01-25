using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Doctor : ApplicationUser
    {

        public int DepartmentID { get; set; }

        //relations below

        public virtual  List<Patient> Patients { get; set; }// One doctor has many patients
        public virtual  List<Visit> Visits { get; set;}// One doctor has many visits

        public virtual Department Department { get; set; } // 



    }
}
