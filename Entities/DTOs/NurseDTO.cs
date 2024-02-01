using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

    public class NurseDTO
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Surname is required")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }

		[Required(ErrorMessage = "Gender is required")]
		public string Gender { get; set; }

		[Required(ErrorMessage = "Blood type is required")]
		public string BloodType { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
    }

}