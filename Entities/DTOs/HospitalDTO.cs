using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

    public class HospitalDTO
    {
        [Required(ErrorMessage = "Hospital Name is required")]
        public string HospitalName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address{ get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNum{ get; set; } // bunu string yap
    }

}
