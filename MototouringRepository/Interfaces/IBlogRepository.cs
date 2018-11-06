using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface IBlogRepository : IRepository<BlogPost>
    {
        void AddBlogPostsFromSource(IEnumerable<BlogPost> blogPosts); 
    }
}
