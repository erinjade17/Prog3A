using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    // Represents a farmer in the AgriEnergyConnect application
    public class Farmer
    {
        // Unique identifier for the farmer
        public int FarmerId { get; set; }

        // Required property for the farmer's name
        [Required]
        public string Name { get; set; }

        // Required property for the farmer's email with email address validation
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Optional property for the farmer's phone number
        public string Phone { get; set; }

        // Optional property for the farmer's location
        public string Location { get; set; }

        // Navigation property to the products associated with the farmer
        // A farmer can have multiple products
        public ICollection<Product> Products { get; set; }
    }
}
