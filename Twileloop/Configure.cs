using Serilog;

namespace Packages.Twileloop
{
    public static class Configure
    {
        /// <summary>
        /// Sets the logger up and running
        /// </summary>
        public static void Serilog(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            const string OUTPUT_TEMPLATE = "{Timestamp:MM/dd/yyyy hh:mm:ss tt} [{Level}] {Message}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithProperty("ServiceName", APIConstants.SERVICENAME)
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: OUTPUT_TEMPLATE)
            .WriteTo.Async(x => x.Seq(serverUrl: APIConstants.SEQ_URL, apiKey: APIConstants.SEQ_KEY))
            .CreateLogger();
        }
    }
}
