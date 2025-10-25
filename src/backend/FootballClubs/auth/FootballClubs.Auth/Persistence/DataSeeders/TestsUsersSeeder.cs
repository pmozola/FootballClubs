using Microsoft.AspNetCore.Identity.Data;

namespace FootballClubs.Auth.Persistence.DataSeeders;

public class TestsUsersSeeder(IRegisterService registerService) : ITestDataSeeder
{
    public void Seed()
    {
        registerService.RegisterAsync(new RegisterRequest { Email = "admin@test.com", Password = "Abc123!" }).Wait();
    }
}

public interface ITestDataSeeder
{
    void Seed();
}