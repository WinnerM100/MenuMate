using System.Configuration;
using System.Text;
using MenuMate.Caching;
using MenuMate.Constants.Exceptions;
using MenuMate.Context;
using MenuMate.Models;
using MenuMate.Services;
using MenuMate.Utilities;
using MenuMate.Utilities.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddSwaggerGen(
            option => {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "MenuMate API", Version = "v1"});
                option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[]{}
                    }
                });
            }
        );

        builder.Services.AddDbContext<ClientContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings").GetSection("MenuMateDB").Get<string>());
        });
        builder.Services.AddSingleton<ClientContext>();

        builder.Services.AddSingleton<SqlConnector>();
        builder.Services.AddSingleton<IRoleService, RoleService>();
        builder.Services.AddSingleton<RoleCache>();
        builder.Services.AddSingleton<RolesSettings>();

        builder.Services.AddScoped<IUserService,UserService>();
        builder.Services.AddScoped<IClientService,ClientService>();

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
        
        builder = addRoleBasedAuthorization(builder);

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

    private static WebApplicationBuilder addRoleBasedAuthorization(WebApplicationBuilder builder)
    {   
        using(ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
        {   
            IRoleService? roleService = serviceProvider.GetService<IRoleService>();

            if(roleService is null)
            {
                throw new NotFoundException("roleService");
            }

            IEnumerable<Role> roles = roleService.GetRoles();

            builder.Services.AddAuthorization(options =>
                {
                    foreach(Role role in roles)
                    {   
                        Console.WriteLine($"Adding role:{role}");
                        options.AddPolicy(role.Name, policy => policy.RequireRole(role.Value));
                    }
                }
            );
        }
        
        return builder;
    }
}
