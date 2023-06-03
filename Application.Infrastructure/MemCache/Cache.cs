using Microsoft.Extensions.Caching.Memory;
using Twileloop.Domain;
using Twileloop.Domain.Exceptions;
using Twileloop.Infrastructure.LiteDB;

namespace Twileloop.Infrastructure.MemCache
{
    public class Cache : ICache
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILiteDBStore dbStore;

        public Cache(IMemoryCache memoryCache, ILiteDBStore dbStore)
        {
            this.memoryCache = memoryCache;
            this.dbStore = dbStore;
        }

        public Blog GetBlog(string slug)
        {
            try
            {
                if (memoryCache.TryGetValue(slug, out Blog blog))
                {
                    return blog;
                }
                var dbBlog = dbStore.GetBlog(slug);
                if (dbBlog is null)
                {
                    throw new BlogNotFoundException(slug);
                }
                memoryCache.Set(slug, dbBlog, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(8)));
                return dbBlog;
            }
            catch (Exception)
            {
                throw new CacheException($"Failed when fetching blog from cache with slug '{slug}'");
            }
        }
    }
}
