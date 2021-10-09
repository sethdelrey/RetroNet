using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace _90sTest.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public ReportController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult PostReporting(string postId)
        {
            try
            {
                //var postIdInt = int.Parse(postId);
                var reportedPost = _context.Posts.AsNoTracking().Where(p => p.PostId.Equals(postId)).Include(p => p.User).FirstOrDefault();
                return View("Post", new ReportPostModel() { Post = reportedPost });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Invalid Post Id!" });
            }
        }

        public IActionResult ReportPost(ReportPostModel data)
        {
            try
            {
                data.ReportedPost.Id = Guid.NewGuid().ToString();
                _context.ReportedPosts.Add(data.ReportedPost);
                _context.SaveChanges();
            } catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Your report did not go through! We are looking into the issue." });
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserReporting(string userId)
        {
            try
            {
                //var userIdInt = int.Parse(userId);
                var reportedUser = _context.Users.AsNoTracking().Where(u => u.Id.Equals(userId)).FirstOrDefault();
                return View("User", new ReportUserModel() { User = reportedUser });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Invalid User Id!" });
            }
        }

        public IActionResult ReportUser(ReportUserModel data)
        {
            try
            {
                data.ReportedUser.Id = Guid.NewGuid().ToString();
                _context.ReportedUsers.Add(data.ReportedUser);
                _context.SaveChanges();
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Your report did not go through! We are looking into the issue." });
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
