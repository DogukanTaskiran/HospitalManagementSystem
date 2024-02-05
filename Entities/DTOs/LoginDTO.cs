using System.ComponentModel.DataAnnotations;
namespace Entities.DTOs{

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [MaxLength(100, ErrorMessage = "Email cannot be longer than 100 characters")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    //[RegularExpression(@"^(?=.*[a-zðüþýöç])(?=.*[A-ZÐÜÞÝÖÇ])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
    //ErrorMessage = "Password must have at least 1 lowercase letter, 1 uppercase letter, 1 digit, 1 special character and 8 characters in total.")]
    public string Password { get; set; }
}

}

