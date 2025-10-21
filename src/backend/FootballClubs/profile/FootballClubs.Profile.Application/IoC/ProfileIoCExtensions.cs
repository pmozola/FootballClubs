using FootballClubs.Profile.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballClubs.Profile.Application.IoC;

public static class ProfileIoCExtensions
{
    public static IServiceCollection AddProfileApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProfilesDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString(ProfilesDbContext.ConnectionStringSettingName)));
        services
            .AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ProfileIoCExtensions)));
        
        return services;
    }
}