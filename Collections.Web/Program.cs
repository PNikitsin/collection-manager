using Collections.Infrastructure.Data;
using Collections.Web.Extension;
using Collections.Web.Services.Implementations;
using Collections.Web.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentification();
builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddRouting(options
    => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

await ApplicationDbContextSeed.SeedEssentialsAsync(app);