using Entities.Models;

namespace Entities.DTOs
{

    public class ReportDTO
    {
        public int PatientID {get;set;}
        public ApplicationUser? ApplicationUser {get;set;}
        public List<Report>? Reports {get;set;}
        public string ReportDescription { get; set; }
        public IFormFile ReportFile { get; set; }
    }

}