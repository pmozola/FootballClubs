using System.Text;
using FootballClubs.Auth.Persistence;
using FootballClubs.Auth.Persistence.DataSeeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FootballClubs.Auth;

public static class AuthExtension
{
    public static IServiceCollection AddFootballClubsAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAuthentication(configuration);

        services
            .AddTransient<IRegisterService, RegisterService>()
            .AddTransient<ILoginService, LoginService>()
            .AddTransient<IJwtKeyService, JwtKeyService>();

        services.AddTransient<ITestDataSeeder, TestsUsersSeeder>();
        
        services.AddDbContext<AuthDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString(AuthDbContext.ConnectionStringSettingName)));
        
        return services;
    }

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>();
        
        services.AddAuthorizationBuilder()
            .AddPolicy(
                "AdminPolicy",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                })
            .AddPolicy(
                "UserPolicy",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("User");
                });
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            var key = Encoding.ASCII.GetBytes(configuration["JwtKey"]!);
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        } );

        return services;
    }
}