using FootballClubs.Profile.Domain;
using FootballClubs.Profile.Persistence;
using MediatR;

namespace FootballClubs.Profile.Application.Commands.CreateProfile;

public class CreateProfileCommandHandler(ProfilesDbContext dbContext) : IRequestHandler<CreateProfileCommand>
{
    public Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = UserProfile.Create(request.FirstName, request.LastName, request.PhoneNumber);
        
        dbContext.UserProfiles.Add(entity);
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}