using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public AdminController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var oneWeekAgo = DateTime.Now.AddDays(-7);
                var data = new AdminModel() { 
                    ReportedPosts = _context.ReportedPosts.Where(rp => rp.ReportTime.CompareTo(oneWeekAgo) > 0).ToList(),
                    ReportedUsers = _context.ReportedUsers.Where(ru => ru.ReportTime.CompareTo(oneWeekAgo) > 0).ToList()
                };

                return View("Admin", data);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "We are looking into the issue." });
            }
        }

        public bool DeleteUser(string userId)
        {
            return false;
        }

        public bool DeletePost(string postId)
        {
            return false;
        }
    }
}
