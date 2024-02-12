using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs{
	public class AnonymousRegisterDTO
	{
		[Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(100, ErrorMessage = "Surname cannot be longer than 100 characters")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters")]
        public string PhoneNumber { get; set; }



    }
}     