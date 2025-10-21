using FootballClubs.Profile.Domain;
using Microsoft.EntityFrameworkCore;

namespace FootballClubs.Profile.Persistence;

public class ProfilesDbContext : DbContext
{
    public static string ConnectionStringSettingName = nameof(ProfilesDbContext);
    public ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
}