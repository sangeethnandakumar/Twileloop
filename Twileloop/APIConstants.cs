namespace Packages.Twileloop
{
    public static class APIConstants
    {
        //Service
        public const string SERVICENAME = "twileloop.com";

        //Logging
        public const string SEQ_URL = "https://seq.twileloop.com";
        public const string SEQ_KEY = "47yKIIsOmtzZBSkN74lW";

        //Security
        public const string SELF = "'self'";
        public const string INLINE = "'unsafe-inline'";
        public const string EVAL = "'unsafe-eval'";
        public const string JSDELIVER = "cdn.jsdelivr.net";
        public const string CLOUDFLARE = "cdnjs.cloudflare.com";
        public const string DATATABLE = "cdn.datatables.net";
        public const string GOOGLE_FONTS = "fonts.googleapis.com";
        public const string GOOGLE_TAG_MANAGER = "www.googletagmanager.com";
        public const string GOOGLE_ANALYTICS = "www.google-analytics.com";
        public const string JQUERY = "code.jquery.com";
        public const string IILI = "iili.io";
        public const string DATA = "data:";
        public const string GSTATIC = "fonts.gstatic.com";
        public const string WEBSOCKET_LOCAL = "wss://localhost:44364";
        public const string WEBSOCKET_REMOTE = "wss://twileloop.com";


        public static Dictionary<string, string[]> CONTENT_SECURITY_POLICY = new Dictionary<string, string[]>
        {
            { "default-src", new string[] { SELF } },
            { "script-src", new string[] { SELF, INLINE, EVAL, JSDELIVER, CLOUDFLARE, JQUERY, GOOGLE_TAG_MANAGER, DATATABLE, GOOGLE_ANALYTICS } },
            { "style-src", new string[] { SELF, INLINE, JSDELIVER, CLOUDFLARE, DATATABLE, GOOGLE_FONTS, JQUERY } },
            { "img-src", new string[] { SELF, DATA, JSDELIVER, IILI, GOOGLE_FONTS } },
            { "font-src", new string[] { SELF, JSDELIVER, GOOGLE_FONTS, CLOUDFLARE, GSTATIC, DATA } },
            { "connect-src", new string[] { SELF, WEBSOCKET_LOCAL, WEBSOCKET_REMOTE, GOOGLE_ANALYTICS } }
        };



    }
}