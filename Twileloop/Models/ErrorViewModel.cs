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
}