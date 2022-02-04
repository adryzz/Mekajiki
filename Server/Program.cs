using Mekajiki.Server.Security;
using Microsoft.AspNetCore.ResponseCompression;
using NodaTime;

namespace Mekajiki.Server;

public static class Program
{
    public static Configuration Config = new();

    public static Instant StartupTime { get; } = SystemClock.Instance.GetCurrentInstant();
    public static void Main(string[] args)
    {
        if (Configuration.Exists("config.json"))
        {
            var v = Configuration.FromFile("config.json");

            Config = v ?? throw new ApplicationException();
        }
        else
        {
            Config.Save("config.json");
        }

        SecurityManager.Initialize();
        
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}
