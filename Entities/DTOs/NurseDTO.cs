using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

    public class NurseDTO
    {
        public int DepartmentID { get; set; }

        public int ApplicationUserID {get;set;}
        
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Surname is required")]
        [MaxLength(100, ErrorMessage = "Surname cannot be longer than 100 characters")]
        public string Surname { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
        [MaxLength(15, ErrorMessage = "Phone number cannot be longer than 15 characters")]
        public string PhoneNumber { get; set; }

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
        //[RegularExpression(@"^(?=.*[a-z������])(?=.*[A-Z������])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
        //ErrorMessage = "Password must have at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and 8 characters in total.")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
    }

}