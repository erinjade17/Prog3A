using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Controllers
{
    // Restrict access to "Farmer" role users
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FarmerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Displays farmer's products
        public async Task<IActionResult> MyProducts()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            var farmer = await GetOrCreateFarmerAsync(user);
            var products = await _context.Products
                                         .Where(p => p.FarmerId == farmer.FarmerId)
                                         .ToListAsync();

            return View(products);
        }

        // Returns the view to add a new product
        public IActionResult AddProduct() => View();

        // Adds a new product
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found.");

            var farmer = await GetOrCreateFarmerAsync(user);
            product.FarmerId = farmer.FarmerId;
            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("❌ Failed to save Product: " + ex.InnerException?.Message ?? ex.Message);
                throw;
            }

            return RedirectToAction("MyProducts");
        }

        // Helper to retrieve or create a farmer
        private async Task<Farmer> GetOrCreateFarmerAsync(ApplicationUser user)
        {
            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (farmer == null)
            {
                farmer = new Farmer
                {
                    Name = user.FullName?.Trim() ?? user.Email,
                    Email = user.Email?.Trim() ?? throw new System.Exception("Missing email"),
                    Phone = "Unknown",
                    Location = "Unknown"
                };

                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
            }

            return farmer;
        }
    }
}
