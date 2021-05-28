using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using _90sTest.Models;
using _90sTest.Entities;
using _90sTest.Data;
using Microsoft.AspNetCore.Identity;
using _90sTest.Areas.Identity.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using _90sTest.Data.Extension;
using Microsoft.EntityFrameworkCore;

namespace _90sTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public HomeController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var postList = _context.Posts.Include(post => post.User).ToList();

                var feed = new FeedModel() {Posts = postList.ToArray()};

                return View("Index", feed);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SubmitAsync(PostsModel data)
        {
            var userId = User.GetLoggedInUserId<string>(); // Specify the type of your UserId;

            RetroNetUser user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<string>());

            // Add new post to db
            _context.Posts.Add(new Post(data.NewPost.Content, user, DateTime.Now));
            _context.SaveChanges();

            // Get posts and users from db
            var postList = _context.Posts.Include(post => post.User).ToList();

            var feed = new FeedModel() { Posts = postList.ToArray() };

            return View("Index", feed);
        }
    }
}
