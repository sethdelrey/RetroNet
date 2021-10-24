using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [PersonalData]
        [MaxLength(256)]
        public string Bio { get; set; }

        [DataType(DataType.Date)]
        public DateTime PasswordResetTime { get; set; }

        public ICollection<Blocks> Followers { get; set; }

        public ICollection<Blocks> Following { get; set; }

        public ICollection<Likes> LikedPosts { get; set; }


        public override bool Equals(object obj)
        {
            return obj != null && UserName.Equals(((RetroNetUser) obj).UserName);
        }
    }
}
