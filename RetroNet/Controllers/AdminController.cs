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
                    ReportedPosts = _context.ReportedPosts.Where(rp => rp.ReportTime.CompareTo(oneWeekAgo) > 0).OrderByDescending(ru => ru.ReportTime).ToList(),
                    ReportedUsers = _context.ReportedUsers.Where(ru => ru.ReportTime.CompareTo(oneWeekAgo) > 0).OrderByDescending(ru => ru.ReportTime).ToList()
                };

                return View("Admin", data);
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "We are looking into the issue." });
            }
        }

        public IActionResult DeleteUser(string userId)
        {
            try
            {
                var user = _context.Users.Find(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                }
                else
                {
                    throw new NullReferenceException();
                }

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "That user does not exist." });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeletePost(string postId)
        {
            try
            {
                var post = _context.Posts.Find(postId);
                if (post != null)
                {
                    _context.Posts.Remove(post);
                    _context.SaveChanges();
                }
                else
                {
                    throw new NullReferenceException();
                }

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "That post does not exist." });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            return RedirectToAction("Index");
        }
    }
}
