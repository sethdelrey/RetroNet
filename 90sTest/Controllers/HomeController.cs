using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _90sTest.Models;
using _90sTest.Entities;
using _90sTest.Data;

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
            using (var context = new FeedContext())
            {
                var postList = context.Post.ToList();
                var userList = context.User.ToList();

                var feed = new FeedModel() { Users = userList.ToArray(), Posts = postList.ToArray() };

                return View("Index", feed);
            }

/*            Post[] posts = new Post[] { new Post("Test", "SethDelRey", DateTime.Now), 
                new Post("Test 2", "alpaulex", DateTime.Now) };

            return View("Index", new PostsModel { Posts = posts});*/
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Submit(PostsModel data)
        {
            using (var context = new FeedContext())
            {
                // Add new post to db
                context.Post.Add(new Post(data.NewPost.Content, "alpaulex", DateTime.Now));
                context.SaveChanges();

                // Get posts and users from db
                var postList = context.Post.ToList();
                var userList = context.User.ToList();

                var feed = new FeedModel() { Users = userList.ToArray(), Posts = postList.ToArray() };

                return View(feed);
            }
        }
    }
}
