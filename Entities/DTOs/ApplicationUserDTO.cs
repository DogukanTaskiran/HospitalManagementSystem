using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs{
	public class ApplicationUserDTO
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

        [Required(ErrorMessage = "Age is required")]
        [Range(0, 120, ErrorMessage = "Invalid age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Height is required (cm)")]
        [Range(0, 250, ErrorMessage = "Invalid height")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Weight is required (kg)")]
        [Range(0, 500, ErrorMessage = "Invalid weight")]
        public int Weight { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [MaxLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }

		[Required(ErrorMessage = "Gender is required")]
		public string Gender { get; set; }


		[Required(ErrorMessage = "Blood type is required")]
		public string BloodType { get; set; }

  
        [Required(ErrorMessage = "Email is required")]                                                          
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-zğüşıöç])(?=.*[A-ZĞÜŞİÖÇ])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
        //ErrorMessage = "Password must have at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and 8 characters in total.")]
        public string Password { get; set; }


		[Required(ErrorMessage = "Confirm password is required")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}