namespace Twileloop.Domain
{
    public class Blog
    {
        public Guid Fingerprint { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ShortDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastEdited { get; set; }
        public List<BlogAuthor> Authors { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<BlogImage> Images { get; set; } = new();
        public List<BlogParagraph> Paragraphs { get; set; } = new();
        public List<BlogAlert> Alerts { get; set; } = new();
        public List<BlogCode> Codes { get; set; } = new();
        public List<BlogComponent> Components { get; set; } = new();
    }
}
