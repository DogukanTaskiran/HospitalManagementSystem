using Entities.Models;

namespace Entities.DTOs
{
    public class PrescriptionDTO
    {

        public int PatientID { get; set; }
        public DateTime PrescriptionDate {get;set;}


        public List<Prescription>? Prescriptions{ get; set; }
        public IFormFile PrescriptionFile { get; set; }

    }
}