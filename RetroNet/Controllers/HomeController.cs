using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
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

            var feed = new FeedModel() { Posts = postList.OrderByDescending(p => p.Date).ToArray() };

            return View("Index", feed);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAsync(FeedModel data)
        {
            var userId = User.GetLoggedInUserId<string>(); // Specify the type of your UserId;

            RetroNetUser user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<string>());

            // Add new post to db
            _context.Posts.Add(new Post(data.NewPost.Content, user, DateTime.Now));
            _context.SaveChanges();
            ModelState.Clear();
            // Get posts and users from db
            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return View("Index", feed);
        }

        public IActionResult Like(string postId)
        {
            var loggedInUserId = User.GetLoggedInUserId<string>();
            var postIdInt = int.Parse(postId);
            var postList = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId == postIdInt).ToArray();
            if (postList != null && postList.Length != 0)
            {
                var count = _context.Likes.Select(likes => likes).Where(likes => likes.Liker.Id.Equals(loggedInUserId) && likes.LikedPostId == postIdInt).ToList().Count;
                if (count == 0)
                {
                    postList[0].Likes++;
                    _context.Posts.Update(postList[0]);


                    _context.Likes.Add(new Likes()
                    {
                        LikerId = loggedInUserId,
                        LikedPostId = postList[0].PostId

                    });
                }
            }

            _context.SaveChanges();

            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return View("Index", feed);
        }

        public IActionResult Delete(string postId)
        {
            var postIdInt = int.Parse(postId);
            var postList = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId == postIdInt && p.User.UserName == User.GetLoggedInUserName()).ToArray();

            if (postList != null && postList.Length != 0)
            {
                _context.Posts.Remove(postList[0]);
            }

            _context.SaveChanges();

            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return View("Index", feed);
        }
    }
}

