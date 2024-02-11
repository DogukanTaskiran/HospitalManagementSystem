namespace Entities.DTOs;
public class OffDutyDTO
{
    public int DoctorID { get; set; } // zaten app user id ye set ediliyor
    public int DepartmentID {get;set;}
    public bool OffDuty { get; set; }
    public DateTime? OffDutyStartDate { get; set; }
    public DateTime? OffDutyEndDate { get; set; }
}