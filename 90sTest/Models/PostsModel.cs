using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _90sTest.Entities;

namespace _90sTest.Models
{
    public class PostsModel
    {
        public Post NewPost { get; set; }
        public Post[] Posts { get; set; }

    }
}
