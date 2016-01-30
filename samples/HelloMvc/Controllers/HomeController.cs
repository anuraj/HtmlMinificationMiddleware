using System.Security.Claims;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc;

namespace MvcSample.Web
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [AllowAnonymous]
        public IActionResult Sample()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public IActionResult Page1()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public IActionResult Page2()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public IActionResult Page3()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public IActionResult Page4()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public IActionResult Page5()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        public Person JsonOutput()
        {
            return new Person() { Id = 10, Name = "Anuraj", Email = "anuraj@ab.com" };
        }

        [AllowAnonymous, HttpPost]
        public IActionResult Login(FormCollection formCollection)
        {
            var claims = new[] { new Claim("name", "Sample User"), new Claim(ClaimTypes.Role, "Admin") };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

            return Redirect("~/Home/Index");
        }
    }
    
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
    }
}