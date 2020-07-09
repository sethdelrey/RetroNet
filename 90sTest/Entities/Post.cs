using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.Entities
{
    public class Post
    {
        public string Content { get; set; }

        public User Creator { get; set; }

        public int Likes { get; set; }

        public DateTime Date { get; set; }

        public Post(string content)
        {
            Content = content;
        }

        public Post(string content, User creator, DateTime date)
        {
            Content = content;
            Creator = creator;
            Date = date;
        }
    }
}
