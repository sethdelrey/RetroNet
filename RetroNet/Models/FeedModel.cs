using _90sTest.Areas.Identity.Data;
using _90sTest.Entities;
using System.ComponentModel.DataAnnotations;

namespace _90sTest.Models
{
    public class FeedModel
    {
        public Post[] Posts { get; set; }
        
        // Top liked posts
        public Post[] HotPosts {get; set; }

        [Required(ErrorMessage = "Your post must include something!")]
        [MaxLength(1000, ErrorMessage = "Posts have a character limit of 1000 :/")]
        public string NewPostContent { get; set; }

        public RetroNetUser CurrentUser { get; set; }
    }
}
