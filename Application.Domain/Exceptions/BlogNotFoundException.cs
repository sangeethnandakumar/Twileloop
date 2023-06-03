namespace Twileloop.Domain.Exceptions
{
    public class BlogNotFoundException : Exception
    {
        public BlogNotFoundException(string identifier) : base(identifier)
        {
        }
    }
}
