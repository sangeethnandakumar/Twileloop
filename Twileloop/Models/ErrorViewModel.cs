namespace Packages.Twileloop.Models
{
    public class ErrorViewModel {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ErrorVM
    {
        public string Referer { get; set; }
    }

    public class Redirect
    {
        public string RedirectFrom { get; set; }
        public string RedirectTo { get; set; }
        public DateTime Event { get; set; }
    }
}