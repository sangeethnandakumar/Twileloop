using Twileloop.Application.Abstractions;
using Twileloop.Domain;
using Twileloop.Infrastructure.LiteDB;
using Twileloop.Infrastructure.MemCache;

namespace Twileloop.Application.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly ICache cache;
        private readonly ILiteDBStore store;

        public BlogService(ICache cache, ILiteDBStore store)
        {
            this.cache = cache;
            this.store = store;
        }

        public Blog GetBlog(string slug)
        {
            return cache.GetBlog(slug);
        }

        public Blog GetBlog(Guid fingerprint)
        {
            return store.GetBlog(fingerprint);
        }
    }
}
