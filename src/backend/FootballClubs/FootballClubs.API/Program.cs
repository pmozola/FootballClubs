using FootballClubs.Auth;
using FootballClubs.Auth.Persistence;
using FootballClubs.Auth.Persistence.DataSeeders;
using FootballClubs.Profile.Application.IoC;
using FootballClubs.Profile.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddOptions<AppAuthorizationOptions>()
    .Bind(builder.Configuration.GetSection(AppAuthorizationOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddFootballClubsAuth(builder.Configuration);
builder.Services.AddProfileApplication(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    List<DbContext> dbContexts =
    [
        scope.ServiceProvider.GetRequiredService<AuthDbContext>(),
        scope.ServiceProvider.GetRequiredService<ProfilesDbContext>()
    ];
    dbContexts.ForEach(x => x.Database.EnsureCreated());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs");
    using var scope = app.Services.CreateScope();
    scope
        .ServiceProvider
        .GetRequiredService<IEnumerable<ITestDataSeeder>>()
        .ToList()
        .ForEach(seeder => seeder.Seed());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();