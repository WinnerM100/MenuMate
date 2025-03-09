using System.Configuration;
using System.Text;
using MenuMate.AccessLayer.Context;
using MenuMate.AccessLayer.Models;
using MenuMate.Security.Authentication;
using MenuMate.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace MenuMate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<MenuMateContext>();

        builder.Services.AddSingleton<SqlConnector>();

        builder.Services.AddSingleton<IAuthenticator, JwtAuthenticator>();
        
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
            option => {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "MenuMate API", Version = "v1"});
                // option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                // {
                //     In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                //     Description = "Please enter a valid token",
                //     Name = "Authorization",
                //     Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                //     BearerFormat = "JWT",
                //     Scheme = JwtBearerDefaults.AuthenticationScheme
                // });
                // option.AddSecurityRequirement(new OpenApiSecurityRequirement
                // {
                //     {
                //         new OpenApiSecurityScheme
                //         {
                //             Reference = new OpenApiReference
                //             {
                //                 Type = ReferenceType.SecurityScheme,
                //                 Id = JwtBearerDefaults.AuthenticationScheme
                //             }
                //         },
                //         new string[]{}
                //     }
                // });
            }
        );

        //authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(
                            options => {
                                options.SaveToken = true;
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

        PopulateRolesFromConfig(builder.Services);

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

    public static void PopulateRolesFromConfig(IServiceCollection services)
    {   
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IRoleService roleService = serviceProvider.GetRequiredService<IRoleService>();

        roleService.PopulateRoleTableFromConfig();
    }
}