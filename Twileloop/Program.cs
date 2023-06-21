using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Net.Http.Headers;
using Packages.Twileloop;
using Twileloop.Middlewares;
using Twileloop.Repository;
using Twileloop.UOW;
using Twileloop.UOW.MongoDB.Support;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);
Configure.Serilog(builder);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddLiveReload();
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
}); 
builder.Services.AddUnitOfWork(options =>
{
    options.Connections = new List<MongoDBConnection>
    {
         new MongoDBConnection("Packages", builder.Configuration.GetConnectionString("Packages"))
    };
});
builder.Services.AddSingleton<DataHandler>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
if (app.Environment.IsDevelopment())
{
    app.UseLiveReload();
}
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        const int durationInSeconds = (60 * 60 * 24) * 90; // 3 months
        ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={durationInSeconds}";
    }
});
app.UseSitemapMiddleware();


//app.Use(async (context, next) =>
//{
//    string cspValue = string.Join("; ", APIConstants.CONTENT_SECURITY_POLICY.Select(directive => directive.Key + " " + string.Join(" ", directive.Value)));
//    context.Response.Headers.Add("Content-Security-Policy", cspValue);
//    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
//    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
//    context.Response.Headers.Add("X-Frame-Options", "sameorigin");
//    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
//    await next();
//});

app.UseRouting();

if (!app.Environment.IsDevelopment())
{
    app.UseResponseCompression();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
