using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

    public class HospitalDTO
    {
        [Required(ErrorMessage = "Hospital Name is required")]
        [MaxLength(100, ErrorMessage = "Hospital name cannot be longer than 100 characters")]
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address{ get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [MaxLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters")]
        public string PhoneNum{ get; set; }
    }

}
