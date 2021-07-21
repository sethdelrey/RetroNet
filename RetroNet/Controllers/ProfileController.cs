using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _90sTest.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public ProfileController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string username)
        {
            var data = new ProfileModel
            {
                User = _userManager.FindByNameAsync(username).Result,
                UsersPosts = _context.Posts.Where(p => p.User.UserName.Equals(username)).Select(p => p).ToList(),
                UserLikedPosts = _context.Likes.Where(likes => likes.Liker.UserName.Equals(username)).Include(rat => rat.LikedPost.User).Select(likes => likes.LikedPost).ToList()

            };

            return View("Index", data);
        }
    }


}
