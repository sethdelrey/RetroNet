using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                var currentUserId = User.GetLoggedInUserId<string>();
                var blockedUserId = _userManager.FindByNameAsync(username).Result.Id;

                var data = new ProfileModel
                {
                    User = _userManager.FindByNameAsync(username).Result,
                    UsersPosts = _context.Posts.AsNoTracking().Where(p => p.User.UserName.Equals(username)).Select(p => p).OrderByDescending(p => p.Date).ToList(),
                    UserLikedPosts = _context.Likes.AsNoTracking().Where(likes => likes.Liker.UserName.Equals(username)).Include(rat => rat.LikedPost.User).Select(likes => likes.LikedPost).OrderByDescending(p => p.Date).ToList(),
                    IsBlocked = _context.Blocks.AsNoTracking().Where(f => f.UserId.Equals(blockedUserId) && f.BlockerId.Equals(currentUserId)).ToList().Count != 0,
                    BlockeByCount = _context.Blocks.AsNoTracking().Where(f => f.UserId.Equals(blockedUserId)).Count(),
                    BlockedCount = _context.Blocks.AsNoTracking().Where(f => f.BlockerId.Equals(blockedUserId)).Count()
                };

                return View("Profile", data);
            } 
            catch (NullReferenceException)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "That user does not exist, please check the username and try again." });
            }
            catch
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorDisplayMessage = "We are looking into the issue." });
            }
        }

        public IActionResult Block(string userId)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var blockedUserId = userId;

            if (!currentUserId.Equals(blockedUserId))
            {
                var result = _context.Blocks.AsNoTracking().Where(b => b.UserId.Equals(blockedUserId) && b.BlockerId.Equals(currentUserId)).ToList();

                if (result.Count == 0)
                {
                    var f = new Blocks() { UserId = blockedUserId, BlockerId = currentUserId };
                    _context.Blocks.Add(f);
                    _context.SaveChanges();
                }

            }

            return RedirectToAction("Profile", new { username = _userManager.FindByIdAsync(blockedUserId).Result.UserName });
        }

        public IActionResult UnBlock(string userId)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var blockedUserId = userId;

            if (!currentUserId.Equals(blockedUserId))
            {
                

                var result = _context.Blocks.AsNoTracking().Where(b => b.UserId.Equals(blockedUserId) && b.BlockerId.Equals(currentUserId)).ToList();

                if (result.Count != 0)
                {
                    var f = new Blocks() { UserId = blockedUserId, BlockerId = currentUserId };
                    _context.Blocks.Remove(f);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Profile", new { username = _userManager.FindByIdAsync(blockedUserId).Result.UserName });
        }
    }


}
