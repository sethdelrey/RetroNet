using _90sTest.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class Post
    {
        [Key]
        [MaxLength(36)]
        public string PostId { get; set; }

        public string Content { get; set; }

        public RetroNetUser User { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Likes> LikedPosts { get; set; }

        public Post()
        {
            Likes = 0;
        }

        public Post(string content)
        {
            Content = content;
        }

        public Post(string _content, RetroNetUser _user, DateTime _date)
        {
            PostId = Guid.NewGuid().ToString();
            Content = _content;
            User = _user;
            Date = _date;
        }
    }
}
