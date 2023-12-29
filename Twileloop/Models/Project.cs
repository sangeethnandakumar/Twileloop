using Twileloop.UOW.MongoDB.Support;

namespace Twileloop.Models
{
    public class Project : EntityBase
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Videos { get; set; }
        public List<string> Images { get; set; }
    }
}