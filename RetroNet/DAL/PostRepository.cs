using _90sTest.Data;
using _90sTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace _90sTest.DAL
{
    public class PostRepository : IPostRepository, IDisposable
    {

        private RetroNetContext context;

        public PostRepository(RetroNetContext context)
        {
            this.context = context;
        }

        public void DeletePost(string postId)
        {
            Post post = context.Posts.Find(postId);
            context.Posts.Remove(post);
        }

        public Post GetPostById(string postId)
        {
            return context.Posts.Find(postId);
        }

        public IEnumerable<Post> GetPosts()
        {
            return context.Posts.ToList();
        }

        public void InsertPost(Post post)
        {
            context.Posts.Add(post);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateStudent(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
        }

        public bool HasUserLikedPost(string postId, string userId)
        {

            var like = context.Likes.Where(l => l.LikerId.Equals(userId) && l.LikedPostId.Equals(postId)).FirstOrDefault();//.Find(postId, userId);
            return like != null;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
