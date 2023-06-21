using Twileloop.UOW.MongoDB.Support;

namespace Twileloop.Models
{
    public class Package : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PackageIcon { get; set; }
        public bool IsBeta { get; set; }
        public string Url { get; set; }
        public string GithubURL { get; set; }
    }
}