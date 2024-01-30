
using Entities.Enums;
using Entities.Interfaces;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUser :  IEntity
    {
        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }
        
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Role {get;set;}
        
        public string? PhoneNumber { get; set; } 
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? BloodType {get; set;}

        public string  Email {get;set;}

        public string Password {get;set;}
        
        public int ApplicationUserID {get;set;}
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //relations below

        public Admin? Admin {get;set;}
        public Patient? Patient{get;set;}
        public Doctor? Doctor { get; set; }
        public Nurse? Nurse { get;set;}
        public Receptionist? Receptionist { get; set; }

    
    }
}
