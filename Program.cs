using System.Configuration;
using System.Text;
using MenuMate.Caching;
using MenuMate.Context;
using MenuMate.Services;
using MenuMate.Utilities;
using MenuMate.Utilities.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MenuMate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ClientContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetSection("MenuMateDB").Get<string>());
        });
        builder.Services.AddSingleton<ClientContext>();

        builder.Services.AddSingleton<SqlConnector>();
        builder.Services.AddSingleton<IRoleService, RoleService>();
        builder.Services.AddSingleton<RoleCache>();
        builder.Services.AddSingleton<RolesSettings>();

        builder.Services.AddScoped<IClientService,ClientService>();


        //authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(
                            options => {
                                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true,
                                    ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
                                    ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
                                };
                            }
                        );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
