using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _90sTest.DAL
{
    public interface IPostRepository : IDisposable
    {
        IEnumerable<Post> GetPosts();
        Post GetPostById(string postId);
        void InsertPost(Post post);
        void DeletePost(string postId);
        void UpdateStudent(Post post);
        bool HasUserLikedPost(string postId, string userId);
        void Save();
    }
}
