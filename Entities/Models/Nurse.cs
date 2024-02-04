using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Nurse : ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NurseID { get; set; }
        public int DepartmentID { get; set; }

        public int ApplicationUserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Department Department { get; set; }  // Reference to Department class

       
    }
}
