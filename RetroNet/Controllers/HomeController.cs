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

        public async Task<IActionResult> Index(
            int? pageNumber)
        {
            var pageSize = 25;
            var userId = User.GetLoggedInUserId<string>();
            var oneWeekAgo = DateTime.Now.AddDays(-1);

            var blockList = _context.Blocks.AsNoTracking()
                .Include(bl => bl.Blocker)
                .Where(bl => bl.UserId.Equals(userId)).Select(bl => bl.Blocker.Id).ToList();

            /* var likeList = _context.Likes.AsNoTracking()
                 .Where(l => l.LikerId.Equals(userId)).ToList();
 */
            var recentList = _context.Posts.AsNoTracking().Include(post => post.User).Include(post => post.LikedPosts).Where(post => !blockList.Any(bl => bl.Equals(post.User.Id)));

            var hotList = recentList.OrderByDescending(p => p.Likes).Take(pageSize);

            var feed = new FeedModel() {
                Posts = await PaginatedList<Post>.CreateAsync(recentList.OrderByDescending(p => p.Date), pageNumber ?? 1, pageSize),
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
            RetroNetUser user = await _userManager.FindByIdAsync(User.GetLoggedInUserId<string>());

            _context.Posts.Add(new Post(data.NewPostContent, user, DateTime.Now));
            _context.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("Index");
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

        [HttpPost]
        public int UnLikeAjax(string postId)
        {
            var loggedInUserId = User.GetLoggedInUserId<string>();
            var post = _context.Posts.Include(p => p.User).ThenInclude(p => p.LikedPosts).Select(p => p).Where(p => p.PostId.Equals(postId)).FirstOrDefault();
            
            if (post != null)
            {
                var count = _context.Likes.Where(likes => likes.Liker.Id.Equals(loggedInUserId) && likes.LikedPostId.Equals(postId)).ToList().Count;
                if (count != 0)
                {
                    post.Likes--;
                    _context.Posts.Update(post);

                    //Likes like = _context.Likes.Find(postId, loggedInUserId);
                    var like = _context.Likes.Where(l => l.LikerId.Equals(loggedInUserId) && l.LikedPostId.Equals(postId)).FirstOrDefault();
                    _context.Likes.Remove(like);

                    /*var removeLikes = new Likes() { LikedPostId = postId, LikerId = loggedInUserId };
                    _context.Likes.Attach(removeLikes);
                    _context.Likes.Remove(removeLikes);*/

                    _context.SaveChanges();
                    return post.Likes;
                }
                else
                {
                    return -1;
                    //throw new Exception("You've never liked this post.");
                }
            }
            else
            {
                return -1;
                //throw new Exception("This post does not exist.");
            }
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

            return RedirectToAction("Index");
        }
    }
}

