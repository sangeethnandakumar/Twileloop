namespace Packages.Twileloop
{
    public static class CDNRoot
    {
        //public const string SHARED = "https://cdn.jsdelivr.net/gh/sangeethnandakumar/CDNRack@master/shared";
        //public const string PROJECT = "https://cdn.jsdelivr.net/gh/sangeethnandakumar/CDNRack@master/twileloop";
        
        public const string BASE = "https://localhost:7000";
        //public const string BASE = "https://twileloop.com";

        public const string SHARED = $"{BASE}/cdn/shared";
        public const string PROJECT = $"{BASE}/cdn/project";
    }
}
