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
            // NEED TO OPTIMIZE THIS
            var userId = User.GetLoggedInUserId<string>();
            var blockListPart1 = _context.Blocks.AsNoTracking().Include(block => block.User).Where(block => block.BlockerId.Equals(userId)).Select(block => block.User.Id).ToList();
            var blockListPart2 = _context.Blocks.AsNoTracking().Include(block => block.Blocker).Where(block => block.UserId.Equals(userId)).Select(block => block.Blocker.Id).ToList();
            
            blockListPart1.AddRange(blockListPart2);

            var blockList = blockListPart1.Distinct().ToList();
            
            // Need to add paging to this so that the hit is smaller and add "hot" posts to it.
            var postList = _context.Posts.AsNoTracking().Include(post => post.User).Where(post => !(blockList.Any(bl => bl.Equals(post.User.Id)))).ToList();

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
            _context.Posts.Add(new Post(data.NewPostContent, user, DateTime.Now));
            _context.SaveChanges();
            ModelState.Clear();
            // Get posts and users from db
            var feed = new FeedModel() { Posts = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).OrderByDescending(p => p.Date).ToArray() };

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
            //return View("Index", feed);
        }
    }
}

