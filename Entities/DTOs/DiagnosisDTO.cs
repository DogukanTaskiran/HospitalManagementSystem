using Entities.Models;

namespace Entities.DTOs{
    public class DiagnosisDTO{

        public List<Diagnosis>? Diagnoses {get;set;}
        public int PatientID {get;set;}

        public ApplicationUser? ApplicationUser {get;set;}
        public string DiagnosisDescription {get;set;}

        public DateTime DiagnosisDate {get;set;}

        public int DoctorID {get;set;}

        

    }

}