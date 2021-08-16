using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _90sTest.Areas.Identity.Data;
using _90sTest.Data;
using _90sTest.Data.Extension;
using _90sTest.Entities;
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
            var currentUserId = User.GetLoggedInUserId<string>();
            var followedUserId = _userManager.FindByNameAsync(username).Result.Id;

            var data = new ProfileModel
            {
                User = _userManager.FindByNameAsync(username).Result,
                UsersPosts = _context.Posts.AsNoTracking().Where(p => p.User.UserName.Equals(username)).Select(p => p).OrderByDescending(p => p.Date).ToList(),
                UserLikedPosts = _context.Likes.AsNoTracking().Where(likes => likes.Liker.UserName.Equals(username)).Include(rat => rat.LikedPost.User).Select(likes => likes.LikedPost).OrderByDescending(p => p.Date).ToList(),
                IsFollowed = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(followedUserId) && f.FollowerId.Equals(currentUserId)).ToList().Count != 0,
                FollowersCount = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(followedUserId)).Count(),
                FollowingCount = _context.Follows.AsNoTracking().Where(f => f.FollowerId.Equals(followedUserId)).Count()
            };

            return View("Index", data);
        }

        public IActionResult Follow(string userId)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var followedUserId = userId;

            if (!currentUserId.Equals(followedUserId))
            {
                var result = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(followedUserId) && f.FollowerId.Equals(currentUserId)).ToList();

                if (result.Count == 0)
                {
                    var f = new Follows() { UserId = followedUserId, FollowerId = currentUserId };
                    //_context.Entry(f).State = EntityState.Detached;
                    _context.Follows.Add(f);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Index", new { username = _userManager.FindByIdAsync(followedUserId).Result.UserName });
        }

        public IActionResult UnFollow(string userId)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var followedUserId = userId;

            if (!currentUserId.Equals(followedUserId))
            {
                

                var result = _context.Follows.AsNoTracking().Where(f => f.UserId.Equals(followedUserId) && f.FollowerId.Equals(currentUserId)).ToList();

                if (result.Count != 0)
                {
                    var f = new Follows() { UserId = followedUserId, FollowerId = currentUserId };
                    //_context.Entry(f).State = EntityState.Detached;
                    _context.Follows.Remove(f);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", new { username = _userManager.FindByIdAsync(followedUserId).Result.UserName });
        }
    }


}
