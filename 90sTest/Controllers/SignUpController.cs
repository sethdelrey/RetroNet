using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _90sTest.Models;
using _90sTest.Entities;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _90sTest.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ILogger<SignUpController> _logger;

        public SignUpController(ILogger<SignUpController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("SignUp");
        }

        public IActionResult SignUp(UserModel data)
        {
            int i = 0;
            if (ModelState.IsValid)
            {
                // Put user in database
                i = 25;
                return View("Index");
            }
            System.Console.WriteLine(i);
            return View("SignUp", data);
        }

        public IActionResult SignIn(UserModel data)
        {
            if (ModelState.IsValid)
            {
                return View("Index");
            }

            return View("Signup", data);

        }
    }
}
