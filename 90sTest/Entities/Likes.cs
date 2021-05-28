using _90sTest.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class Likes
    {
        public string LikerId { get; set; }
        public RetroNetUser Liker { get; set; }

        public int LikedPostId { get; set; }
        public Post LikedPost { get; set; }


    }
}
