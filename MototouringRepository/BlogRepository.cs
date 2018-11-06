using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Documents.Client;
using Repository.Interfaces;

namespace Repository
{
    public class BlogRepository : Repository<BlogPost>, IBlogRepository
    {
        public BlogRepository(DocumentClient client, Uri dbCollection) : base(client, dbCollection)
        {
        }

        public void AddBlogPostsFromSource(IEnumerable<BlogPost> blogPosts)
        {
            throw new NotImplementedException();
        }
    }
}
