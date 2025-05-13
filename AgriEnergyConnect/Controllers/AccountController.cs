using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AgriEnergyConnect.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AgriEnergyConnect.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;  // Handles user-related operations
        private readonly SignInManager<ApplicationUser> _signInManager;  // Manages user sign-ins
        private readonly RoleManager<IdentityRole> _roleManager;  // Manages roles in the system
        private readonly ILogger<AccountController> _logger;  // Used for logging actions for auditing and troubleshooting

        // Constructor to inject required services for user management and logging
        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // GET method for the Login page
        [HttpGet]
        public IActionResult Login() => View();

        // POST method for handling the login process
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // If model state is invalid (e.g., missing fields), return the view with validation errors
            if (!ModelState.IsValid)
                return View(model);

            // Attempt to sign in the user with the provided credentials
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            // If sign-in is successful
            if (result.Succeeded)
            {
                // Find the user and their roles
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                // Log the successful login with the user's roles
                _logger.LogInformation($"✅ User {user.Email} logged in with roles: {string.Join(", ", roles)}");

                // Redirect to different actions based on the user's role
                if (roles.Contains("Farmer"))
                    return RedirectToAction("Index", "Home");

                if (roles.Contains("Employee"))
                    return RedirectToAction("ViewAllFarmers", "Employee");

                // Default redirect if no specific roles are found
                return RedirectToAction("Index", "Home");
            }

            // If sign-in fails, add an error message to the ModelState
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        // GET method for the Register page
        [HttpGet]
        public IActionResult Register() => View();

        // POST method for handling the registration process
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // If model state is invalid (e.g., missing fields), return the view with validation errors
            if (!ModelState.IsValid)
                return View(model);

            // Create a new ApplicationUser instance with provided details
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            // Attempt to create the user in the system with the provided password
            var result = await _userManager.CreateAsync(user, model.Password);

            // If user creation is successful
            if (result.Succeeded)
            {
                // If the specified role doesn't exist, create it
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                // Assign the user to the specified role
                await _userManager.AddToRoleAsync(user, model.Role);

                // Sign in the newly registered user
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirect based on the role of the user
                if (model.Role == "Farmer")
                    return RedirectToAction("Index", "Home");

                if (model.Role == "Employee")
                    return RedirectToAction("ViewAllFarmers", "Employee");

                return RedirectToAction("Index", "Home");
            }

            // If user creation fails, add the error messages to ModelState
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            // Return the registration view with validation errors
            return View(model);
        }

        // POST method for handling user logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Log the logout event
            _logger.LogInformation("User logged out.");

            // Redirect to the home page after logout
            return RedirectToAction("Index", "Home");
        }
    }
}
