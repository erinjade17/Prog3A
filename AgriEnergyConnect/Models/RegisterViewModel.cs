using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    // ViewModel for user registration
    public class RegisterViewModel
    {
        // User's full name
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        // User's email address
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // User's password
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        // Confirmation of the password
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        // Role assigned to the user (Farmer or Employee)
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
