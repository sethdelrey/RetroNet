using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Models
{
    public class FollowsModel
    {
        public List<RetroNetUser> FollowersList { get; set; }

        public List<RetroNetUser> FollowingList { get; set; }

        public RetroNetUser User { get; set; }

        public bool IsFollowed { get; set; }
    }
}
