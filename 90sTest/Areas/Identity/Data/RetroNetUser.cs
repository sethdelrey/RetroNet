using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _90sTest.Entities;
using Microsoft.AspNetCore.Identity;

namespace _90sTest.Areas.Identity.Data
{
    public class RetroNetUser : IdentityUser
    {
        public RetroNetUser() : base()
        { }

        public RetroNetUser(string username) : base(username)
        { }

        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public DateTime DOB { get; set; }

        public ICollection<Follows> Followers { get; set; }

        public ICollection<Follows> Following { get; set; }

        public ICollection<Likes> LikedPosts { get; set; }

        public override bool Equals(object obj)
        {
            return UserName.Equals(((RetroNetUser) obj).UserName);
        }
    }
}
