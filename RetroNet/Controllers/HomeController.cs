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
            var userId = User.GetLoggedInUserId<string>();
            var oneWeekAgo = DateTime.Now.AddDays(-1);

            var blockList = _context.Blocks.AsNoTracking()
                .Include(bl => bl.Blocker)
                .Where(bl => bl.UserId.Equals(userId)).Select(bl => bl.Blocker.Id).ToList();
            
            var recentList = _context.Posts.AsNoTracking().Include(post => post.User).Where(post => !blockList.Any(bl => bl.Equals(post.User.Id))).ToList();

            var hotList = recentList.OrderByDescending(p => p.Likes).Take(25);

            var feed = new FeedModel() {
                Posts = recentList.OrderByDescending(p => p.Date).ToArray(),
                HotPosts = hotList.ToArray()
            };

            return View("Home", feed);
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
            _context.Posts.Add(new Post(data.NewPostContent, user, DateTime.Now));
            _context.SaveChanges();
            ModelState.Clear();
            // Get posts and users from db
            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return RedirectToAction("Home");
        }

        [HttpPost]
        public int LikeAjax(string postId)
        {
            
            var loggedInUserId = User.GetLoggedInUserId<string>();
            var post = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId.Equals(postId)).FirstOrDefault();
            
            if (post != null)
            {
                var count = _context.Likes.Where(likes => likes.Liker.Id.Equals(loggedInUserId) && likes.LikedPostId.Equals(postId)).ToList().Count;
                if (count == 0)
                {
                    post.Likes++;
                    _context.Posts.Update(post);


                    _context.Likes.Add(new Likes()
                    {
                        LikerId = loggedInUserId,
                        LikedPostId = post.PostId
                    });

                    _context.SaveChanges();
                    return post.Likes;
                }
                else
                {
                    return -1;
                    //throw new Exception("You've already liked this post.");
                }
            }
            else
            {
                return -1;
                //throw new Exception("This post does not exist.");
            }
        }

        public IActionResult Like(string postId)
        {
            var loggedInUserId = User.GetLoggedInUserId<string>();
            //var postIdInt = int.Parse(postId);
            var postList = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId.Equals(postId)).ToArray();
            if (postList != null && postList.Length != 0)
            {
                var count = _context.Likes.Select(likes => likes).Where(likes => likes.Liker.Id.Equals(loggedInUserId) && likes.LikedPostId.Equals(postId)).ToList().Count;
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

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string postId)
        {
            //var postIdInt = int.Parse(postId);
            var postList = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId.Equals(postId) && p.User.UserName == User.GetLoggedInUserName()).ToArray();

            if (postList != null && postList.Length != 0)
            {
                _context.Posts.Remove(postList[0]);
            }

            _context.SaveChanges();

            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return RedirectToAction("Index");
        }
    }
}

