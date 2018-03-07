using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CreativeCravings.DAL;
using System.Data.Entity;

namespace CreativeCravings.Models
{
    public class EFPostRepository : IPostRepository
    {
        RecipeContext context = new RecipeContext();

        public IQueryable<Post> Posts
        {
            get
            {
                return context.Posts;
            }
        }

        public void Delete(Post post)
        {
            context.Posts.Remove(post);
            context.SaveChanges();
        }

        public Post Save(Post post)
        {
            if (post.ID == 0)
            {
                context.Posts.Add(post);
            }
            else
            {
                context.Entry(post).State = EntityState.Modified;
            }

            context.SaveChanges();
            return post;
            
        }
    }
}