using Packages.Twileloop.Models;

namespace Packages.Twileloop.ViewModels
{
    public class PackageViewModel
    {
        public List<PackageInfo> RecommendedPackages { get; set; }
        public PackageInfo ActivePackage { get; set; }
    }
}
