using System.Configuration;
using MenuMate.Context;
using MenuMate.Services;
using MenuMate.Utilities.Sql;

namespace MenuMate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<SqlConnector>();
        builder.Services.AddScoped<IClientService,ClientService>();
        builder.Services.AddScoped<ClientContext>();

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
