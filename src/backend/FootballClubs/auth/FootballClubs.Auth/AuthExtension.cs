using System.Text;
using FootballClubs.Auth.Persistence;
using FootballClubs.Auth.Persistence.DataSeeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        services
            .AddTransient<IRegisterService, RegisterService>()
            .AddTransient<ILoginService, LoginService>()
            .AddTransient<IJwtKeyService, JwtKeyService>();

        services.AddTransient<ITestDataSeeder, TestsUsersSeeder>();
        
        services.AddDbContext<AuthDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString(AuthDbContext.ConnectionStringSettingName)));
        services.ConfigureAuthentication(configuration);
        return services;
    }

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();
        
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration[$"{AppAuthorizationOptions.SectionName}:{nameof(AppAuthorizationOptions.SecurityKey)}"]!))
                    };
                }
            );
        

        return services;
    }
}