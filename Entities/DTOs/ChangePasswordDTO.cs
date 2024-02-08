using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-zğüşıöç])(?=.*[A-ZĞÜŞİÖÇ])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
        //ErrorMessage = "Password must have at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and 8 characters in total.")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
