using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    // Ensure only users with the "Employee" role can access this controller
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        // Declare a private readonly field for the application context (database access)
        private readonly ApplicationDbContext _context;

        // Constructor to inject the database context into the controller
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Display the form to add a new farmer
        public IActionResult AddFarmer() => View();

        // POST: Handle the form submission for adding a new farmer
        [HttpPost]
        public async Task<IActionResult> AddFarmer(Farmer farmer)
        {
            // Check if the model state is valid (i.e., all required fields are filled correctly)
            if (ModelState.IsValid)
            {
                // Add the new farmer to the database context
                _context.Farmers.Add(farmer);

                // Save changes to the database asynchronously
                await _context.SaveChangesAsync();

                // Redirect to the "ViewAllFarmers" action to see the list of farmers
                return RedirectToAction("ViewAllFarmers");
            }

            // If the model state is not valid, return the view with the current farmer data
            return View(farmer);
        }

        // GET: Retrieve and display all farmers from the database along with their products
        public IActionResult ViewAllFarmers()
        {
            // Retrieve all farmers and include their associated products using eager loading
            var farmers = _context.Farmers.Include(f => f.Products).ToList();

            // Return the list of farmers to the view
            return View(farmers);
        }

        // GET: Filter products based on category and date range (optional)
        public IActionResult FilterProducts(string category, DateTime? fromDate, DateTime? toDate)
        {
            // Start with the products from the database and include the associated farmer
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            // Apply the category filter if a category is provided
            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            // Apply the fromDate filter if a starting date is provided
            if (fromDate.HasValue)
                products = products.Where(p => p.ProductionDate >= fromDate);

            // Apply the toDate filter if an ending date is provided
            if (toDate.HasValue)
                products = products.Where(p => p.ProductionDate <= toDate);

            // Return the filtered list of products to the "FilteredProducts" view
            return View("FilteredProducts", products.ToList());
        }
    }
}
