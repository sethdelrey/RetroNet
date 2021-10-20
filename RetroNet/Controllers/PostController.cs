using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
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
    public class PostController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public PostController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string postId)
        {
            try
            {
                var postFind = _context.Posts.AsNoTracking().Where(p => p.PostId.Equals(postId)).Include(p => p.User).Include(p => p.LikedPosts).FirstOrDefault();

                return View("Post", new PostModel() { Post = postFind} );
            }
            catch (NullReferenceException)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "That post does not exist, sorry!" });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "We are looking into the issue." });
            }
            
        }
    }
}
