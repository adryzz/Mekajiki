using Mekajiki.Server.Security;

namespace Mekajiki.Server;

public static class Program
{
    public static Configuration Config = new();

    public static DateTime StartupTime { get; } = DateTime.Now;

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

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}