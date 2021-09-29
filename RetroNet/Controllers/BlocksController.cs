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
    public class BlocksController : Controller
    {
        private readonly UserManager<RetroNetUser> _userManager;
        private readonly RetroNetContext _context;

        public BlocksController(UserManager<RetroNetUser> userManager, RetroNetContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index(string username)
        {
            var currentUserId = User.GetLoggedInUserId<string>();
            var blockedUserId = _userManager.FindByNameAsync(username).Result.Id;
            var user = _userManager.FindByNameAsync(username).Result;

            var data = new BlocksModel()
            { 
                BlockedByList = _context.Blocks.AsNoTracking().Where(f => f.UserId.Equals(user.Id)).Include(f => f.Blocker).Select(f => f.Blocker).ToList(),
                BlockedList = _context.Blocks.AsNoTracking().Where(f => f.BlockerId.Equals(user.Id)).Include(f => f.User).Select(f => f.User).ToList(),
                User = user,
                IsBlocked = _context.Blocks.AsNoTracking().Where(f => f.UserId.Equals(user.Id) && f.BlockerId.Equals(currentUserId)).ToList().Count != 0,
                BlockeByCount = _context.Blocks.AsNoTracking().Where(f => f.UserId.Equals(blockedUserId)).Count(),
                BlockedCount = _context.Blocks.AsNoTracking().Where(f => f.BlockerId.Equals(blockedUserId)).Count()
            };

            return View("Blocks", data);
        }
    }
}
