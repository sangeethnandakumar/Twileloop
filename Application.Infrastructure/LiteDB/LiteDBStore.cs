using Twileloop.Domain;
using Twileloop.Domain.Exceptions;
using Twileloop.UOW;

namespace Twileloop.Infrastructure.LiteDB
{
    public class LiteDBStore : ILiteDBStore
    {
        private readonly UnitOfWork uow;

        public LiteDBStore(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public Blog GetBlog(Guid fingerprint)
        {
            try
            {
                uow.UseDatabase("Blogs");
                var repo = uow.GetRepository<Blog>();
                var blog = repo.Find(x => x.Fingerprint == fingerprint).FirstOrDefault();
                if (blog is null)
                {
                    throw new BlogNotFoundException($"Unable to find blog with fingerprint '{fingerprint}'");
                }
                return blog;
            }
            catch
            {
                throw new StoreException($"Failed when working with store 'Blogs'");
            }
        }

        public Blog GetBlog(string slug)
        {
            try
            {
                uow.UseDatabase("Blogs");
                var repo = uow.GetRepository<Blog>();
                var blog =  repo.Find(x=>x.Slug.ToLower() == slug.ToLower()).FirstOrDefault();
                if(blog is null)
                {
                    throw new BlogNotFoundException(slug);
                }
                return blog;
            }
            catch
            {
                throw new StoreException($"Failed when working with store 'Blogs'");
            }
        }
    }
}
