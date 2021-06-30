using System;
using System.Collections.Generic;
using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;

namespace _90sTest.Models
{
    public class ProfileModel
    {
        public ProfileModel()
        {
        }

        public RetroNetUser user { get; set; }

        public List<Post> usersPosts { get; set; }
    }
}
