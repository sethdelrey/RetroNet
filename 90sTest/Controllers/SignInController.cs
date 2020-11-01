using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _90sTest.Models;
using _90sTest.Entities;
using System.Reflection.Metadata.Ecma335;

namespace _90sTest.Controllers
{
    public class SignInController : Controller
    {
        private readonly ILogger<SignUpController> _logger;

        public SignInController(ILogger<SignUpController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("SignIn");
        }

        public IActionResult Submit(SignInModel data)
        {
            int i = 0; 
            if (ModelState.IsValid)
            {
                // Put user in database
                i = 25;
                return View("Home/Index");
            }
            System.Console.WriteLine(i);
            return View("SignIn", data);
        }
    }
}
