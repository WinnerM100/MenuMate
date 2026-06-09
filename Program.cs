using System.Text;
using MenuMate.AccessLayer.Context;
using MenuMate.AccessLayer.Models;
using MenuMate.Security.Authentication;
using MenuMate.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MenuMate.Middleware.Security;
using Microsoft.AspNetCore.Authorization;
using MenuMate.Security.Authorization;
using MenuMate.Models;
using MenuMate.Configuration.HttpClientConfig;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace MenuMate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

        // Add services to the container.
        builder.WebHost.UseUrls("https://localhost:7009");
        builder.Services.AddDbContext<MenuMateContext>();

        builder.Services.AddSingleton<SqlConnector>();

        builder.Services.AddSingleton<IAuthenticator, JwtAuthenticator>();
        builder.Services.AddSingleton<MethodAuthorizationCollection>();
        builder.Services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();

        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IRezervareService, RezervareService>();
        
        builder.Services.AddHttpLogging(
            options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            }
        );

        builder = RegisterHttpClients(builder);

        builder.Services.AddControllers().AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }
        );
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
            option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "MenuMate API", Version = "v1" });
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
        //authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(
                            options =>
                            {
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

        //app.UseAuthorizationMiddleware();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpLogging();

        app.MapControllers();

        app.Run();
    }

    public static void PopulateRolesFromConfig(IServiceCollection services)
    {
        ServiceProvider serviceProvider = services.BuildServiceProvider();

        IRoleService roleService = serviceProvider.GetRequiredService<IRoleService>();

        roleService.PopulateRoleTableFromConfig();
    }
    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryAttempts = 1)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                        retryAttempt)));
    }
    public static WebApplicationBuilder RegisterHttpClients(WebApplicationBuilder builder)
    {
        List<HttpClientSettings> httpClients = new List<HttpClientSettings>();

        foreach (var httpConfig in builder.Configuration.GetSection("RemoteServices").GetChildren())
        {
            httpClients.Add(new HttpClientSettings(httpConfig));
        }

        foreach (HttpClientSettings httpClientSettings in httpClients)
        {
            IHttpClientBuilder httpClientBuilder = builder.Services.AddHttpClient(
                httpClientSettings.ServiceName,
                (serviceProvider, client) =>
                {
                    client.BaseAddress = new Uri(httpClientSettings.BaseURL);
                }
            ).ConfigurePrimaryHttpMessageHandler(
                () =>
                {
                    return new SocketsHttpHandler()
                    {
                        PooledConnectionLifetime = HttpOptions.ConvertToTimeSpan(httpClientSettings.HttpOptions.Timeout),
                    };
                }
            );
            // if (httpClientSettings.HttpOptions.Retries > 0)
            // {
            //     httpClientBuilder.AddPolicyHandler(GetRetryPolicy(httpClientSettings.HttpOptions.Retries));
            // }
        }
        
        return builder;
    }
}