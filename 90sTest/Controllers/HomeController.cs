using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _90sTest.Models;
using _90sTest.Entities;

namespace _90sTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            Post[] posts = new Post[] { new Post("Test", new User("SethDelRey", "Seth", "Richard"), DateTime.Now), 
                new Post("Test 2", new User("alpaulex", "Alex", "Marc"), DateTime.Now) };

            return View("Index", new PostsModel { Posts = posts});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Submit(PostsModel data)
        {
            // Add post to Database
            return View("Index");
        }
    }
}
