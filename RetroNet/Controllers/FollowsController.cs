using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Data.Extension;
using _90sTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Controllers
{
    [Authorize]
    public class FollowsController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public FollowsController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string username)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var user = _userManager.FindByNameAsync(username).Result;
            var data = new FollowsModel()
            { 
                FollowersList = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(user.Id)).Include(f => f.Follower).Select(f => f.Follower).ToList(),
                FollowingList = _context.Follows.AsNoTracking().Where(f => f.FollowerId.Equals(user.Id)).Include(f => f.User).Select(f => f.User).ToList(),
                User = user,
                IsFollowed = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(user.Id) && f.FollowerId.Equals(currentUserId)).ToList().Count != 0
            };

            return View("Follows", data);
        }
    }
}
