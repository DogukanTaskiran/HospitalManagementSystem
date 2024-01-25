using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
    public class Admin 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string Gender { get; set; }

        public string BloodType {get; set;}
        public int AdminID {get;set;}
        //one to one relationship with user

        public int ApplicationUserID {get; set;}
        public ApplicationUser ApplicationUser {get; set;}

    
    }
}
