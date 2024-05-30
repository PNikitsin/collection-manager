using Collections.Infrastructure.Data;
using Collections.Web.Application.AutoMapper;
using Collections.Web.Application.Services.Implementations;
using Collections.Web.Application.Services.Interfaces;
using Collections.Web.Extension;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentification(builder.Configuration);
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddRouting(options
    => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews();

var app = builder.Build();

await ApplicationDbContextSeed.SeedEssentialsAsync(app);

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