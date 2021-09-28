using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;

namespace _90sTest.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
        }

        public RetroNetUser User { get; set; }

        public List<Post> UsersPosts { get; set; }

        public List<Post> UserLikedPosts { get; set; }

        public bool IsBlocked { get; set; }

        public int BlockeByCount { get; set; }

        public int BlockedCount { get; set; }
    }
}
