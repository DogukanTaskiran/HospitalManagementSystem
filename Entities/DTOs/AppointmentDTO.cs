using Entities.Models;
using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

    public class AppointmentDTO{

    public List<Entities.Models.Hospital> Hospitals { get; set; }
    public List<Department> Departments { get; set; }
    public List<Doctor> Doctors { get; set; }

    public List<Appointment> availableAppointments {get;set;}

    public int SelectedHospitalId {get;set;}
    public int SelectedDepartmentId {get;set;}

    public int SelectedDoctorId {get;set;}

    public DateTime SelectedDate {get;set;}

    public int AppointmentID {get;set;}
    public int PatientID{get;set;}
        
    }
}