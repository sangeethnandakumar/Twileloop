using Microsoft.Extensions.Caching.Memory;
using Packages.Twileloop.Helpers;
using Twileloop.Models;
using Twileloop.UOW.MongoDB.Core;

namespace Twileloop.Repository
{
    public class DataHandler
    {
        private readonly IMemoryCache memoryCache;
        private readonly UnitOfWork uow;

        public DataHandler(IMemoryCache memoryCache, UnitOfWork uow)
        {
            this.memoryCache = memoryCache;
            this.uow = uow;
        }

        public async Task<List<Package>> GetAllPackages()
        {
            if (memoryCache.TryGetValue("PACKAGE_CATALOGUE", out List<Package> packages))
            {
                return packages.Shuffle().ToList();
            }
            var packageRepo = uow.GetRepository<Package>();
            packages = packageRepo.GetAll().ToList();
            memoryCache.Set("PACKAGE_CATALOGUE", packages, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2)
            });
            return packages.Shuffle().ToList();
        }
    }
}
