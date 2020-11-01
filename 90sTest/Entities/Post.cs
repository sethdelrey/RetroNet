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
        public int PostId { get; set; }
        public string Content { get; set; }

        public string Username { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public Post()
        {
            Likes = 0;
        }

        public Post(string content)
        {
            Content = content;
        }

        public Post(string content, string username, DateTime date)
        {
            Content = content;
            Username = username;
            Date = date;
        }
    }
}
