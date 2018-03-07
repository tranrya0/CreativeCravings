using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        Post Save(Post post);
        void Delete(Post post);
    }
}