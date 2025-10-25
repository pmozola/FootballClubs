using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FootballClubs.Profile.Persistence;

public class DesignTimeAuthDbContext : IDesignTimeDbContextFactory<ProfilesDbContext>
{
    public ProfilesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProfilesDbContext>();
        optionsBuilder.UseSqlServer();

        return new ProfilesDbContext(optionsBuilder.Options);
    }
}