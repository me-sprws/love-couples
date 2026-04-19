using CouplesService.WebAPI.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Serilog;

namespace CouplesService.WebAPI;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApi(
        this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder builder)
    {
        services.AddOpenApi();
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new()
            {
                Title = "CouplesService API",
                Version = "v1"
            });
        });
        
        builder.Host.UseSerilog((context, _, config) => 
            config.ReadFrom.Configuration(context.Configuration));
        
        services.AddAuthorization();
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = configuration.GetValue<string>("Authentication:Google:ClientId")
                    ?? throw new InvalidOperationException("Has no Google OAuth ClientID");
                
                options.ClientSecret = configuration.GetValue<string>("Authentication:Google:ClientSecret")
                    ?? throw new InvalidOperationException("Has no Google OAuth ClientSecret");
                
                options.Scope.Add("email");
                options.Scope.Add("profile");

                options.Events.OnCreatingTicket = AuthenticationHandler.OnCreatingTicket;
            });
        
        return services;
    }
}