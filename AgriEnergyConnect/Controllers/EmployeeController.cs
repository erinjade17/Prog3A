using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AddFarmer() => View();

        [HttpPost]
        public async Task<IActionResult> AddFarmer(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewAllFarmers");
            }
            return View(farmer);
        }

        public IActionResult ViewAllFarmers()
        {
            var farmers = _context.Farmers.Include(f => f.Products).ToList();
            return View(farmers);
        }

        public IActionResult FilterProducts(string category, DateTime? fromDate, DateTime? toDate)
        {
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.Category == category);

            if (fromDate.HasValue)
                products = products.Where(p => p.ProductionDate >= fromDate);

            if (toDate.HasValue)
                products = products.Where(p => p.ProductionDate <= toDate);

            return View("FilteredProducts", products.ToList());
        }
    }
}