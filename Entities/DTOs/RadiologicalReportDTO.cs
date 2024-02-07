using Entities.Models;

namespace Entities.DTOs
{
    public class RadiologicalReportDTO
    {

        public int PatientID { get; set; }
        
        public string RrDescription {get;set;}
        public List<RadiologicalReport>? RadiologicalReports{ get; set; }
        public IFormFile RadiologicalReportFile { get; set; }

    }
}