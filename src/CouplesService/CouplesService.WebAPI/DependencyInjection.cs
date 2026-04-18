using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace CouplesService.WebAPI;

internal static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers();
        
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
            });
        
        return services;
    }
}