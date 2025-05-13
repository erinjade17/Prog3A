using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    // Represents a product in the system
    public class Product
    {
        // Product ID (primary key)
        public int ProductId { get; set; }

        // Name of the product
        [Required]
        public string Name { get; set; }

        // Category of the product
        public string Category { get; set; }

        // Production date of the product
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        // Foreign key to the Farmer who owns the product
        [ForeignKey("Farmer")]
        public int FarmerId { get; set; }

        // Navigation property to the Farmer
        public Farmer Farmer { get; set; }
    }
}
