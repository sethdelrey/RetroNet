using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
                var postIdInt = int.Parse(postId);
                var reportedPost = _context.Posts.AsNoTracking().Where(p => p.PostId == postIdInt).Include(p => p.User).FirstOrDefault();
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
                data.ReportedPost.Id = Guid.NewGuid();
                _context.ReportedPosts.Add(data.ReportedPost);
                _context.SaveChanges();
            } catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Your report did not go through! We are looking into the issue." });
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UserReporting()
        {
            return View("User");
        }
    }
}
