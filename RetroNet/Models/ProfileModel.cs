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

        [Required]
        [MinLength(0)]
        [MaxLength(256)]
        [Display(Name = "New Bio")]
        public string Bio { get; set; }
    }
}
