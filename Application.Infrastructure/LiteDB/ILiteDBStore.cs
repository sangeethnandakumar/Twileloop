using Twileloop.Domain;

namespace Twileloop.Infrastructure.LiteDB
{
    public interface ILiteDBStore
    {
        Blog GetBlog(string slug);
        Blog GetBlog(Guid fingerprint);
    }
}
