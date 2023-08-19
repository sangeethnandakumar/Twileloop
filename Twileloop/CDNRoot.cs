using Microsoft.Extensions.Options;

namespace Packages.Twileloop
{
    public class CDNRoot
    {
        public string SHARED = "";
        public string PROJECT = "";
        public string TWILELOOP_PACKAGES = "";

        public string GITHUB = "";
        public string LINKEDIN = "";
        public string INSTAGRAM = "";
        public string YOUTUBE = "";
        public CDNRoot(IOptions<DomainConfig> domainConfig, IOptions<SocialConfig> socialConfig)
        {
            //Domain Config
            TWILELOOP_PACKAGES = domainConfig.Value.TwileloopPackages;
            SHARED = $"/cdn/shared";
            PROJECT = $"/cdn/project";
            //Social Config
            GITHUB = socialConfig.Value.GitHub;
            LINKEDIN = socialConfig.Value.LinkedIn;
            INSTAGRAM = socialConfig.Value.Instagram;
            YOUTUBE = socialConfig.Value.YouTube;
        }
    }
}
