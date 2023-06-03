using Twileloop.Domain;

namespace Twileloop.Infrastructure.MemCache
{
    public interface ICache
    {
        Blog GetBlog(string slug);
    }
}
