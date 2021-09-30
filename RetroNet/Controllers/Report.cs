using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Controllers
{
    public class Report : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

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
                var reportedPost = _context.Posts.Where(p => p.PostId == postIdInt).FirstOrDefault();
                return View("Post", new ReportPostModel() { ReportedPost = reportedPost });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "Invalid Post Id!" });
            }
        }

        public IActionResult UserReporting()
        {
            return View("User");
        }
    }
}
