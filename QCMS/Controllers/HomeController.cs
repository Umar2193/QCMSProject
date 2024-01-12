using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using QCMS.Models;
using Repository.Company;
using System.Diagnostics;
using System.Security.Claims;
using System.Reflection;

namespace QCMS.Controllers
{
    public class HomeController : Controller
    {
		private ICompanyRepository _companyRepository = new CompanyRepository();
		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //RedirectToAction("Index", "Company");
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult AddProduct()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		public ActionResult Login()
		{
			return View();
		}
        [HttpPost]
		public ActionResult Login(string Username,string Password)
		{
			ViewBag.Error = "Login Validation Failed...!!!";
			if (Username == null || Password == null)
            {
                
                return View();
            }

            else
            {
                if(_companyRepository.ValidateLogin(Username, Password))
				{
					var claims = new List<Claim>
					{
					new Claim(ClaimTypes.Name, Username),
                    // You can add more claims here if needed
                     };

					// Create a ClaimsIdentity and a ClaimsPrincipal with the claims
					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        // Set other properties as needed

                        // Set the expiration time for the cookie
                        ExpiresUtc = DateTime.UtcNow.AddDays( 1440), // Adjust the expiration time based on rememberMe
                        IsPersistent = true
                    };

                    // Sign in the user with the claimsPrincipal
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,authProperties);
 
					return RedirectToAction("Index", "Company");
				}
                return View();
			}
           
			
		}
		public ActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Home");

		}
	}
}