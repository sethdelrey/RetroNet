using _90sTest.Entities;

namespace _90sTest.Models
{
    public class FeedModel
    {
        public Post[] Posts { get; set; }

        public User[] Users { get; set; }

        public Post NewPost { get; set; }
    }
}
