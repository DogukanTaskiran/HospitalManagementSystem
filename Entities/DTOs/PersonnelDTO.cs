using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Microsoft.Identity.Client;
namespace Entities.DTOs{

    public class PersonnelDTO
    {   
        public int DepartmentID {get;set;}
        public List<Doctor>? Doctors {get;set;}
        public List<Nurse>? Nurses {get;set;}
    }

}
