namespace Twileloop.Domain
{
    public class BlogAlert : BlogComponent
    {
        public string Heading { get; set; }
        public string Alert { get; set; }
        public BlogAlertTypes Type { get; set; }
    }
}
