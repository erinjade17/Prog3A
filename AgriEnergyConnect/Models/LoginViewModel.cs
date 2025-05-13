using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    // ViewModel for user login
    public class LoginViewModel
    {
        // Email field with validation
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        // Password field with validation
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Remember me option
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
