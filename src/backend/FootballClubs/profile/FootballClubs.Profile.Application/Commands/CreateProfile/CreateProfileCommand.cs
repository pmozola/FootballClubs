using MediatR;

namespace FootballClubs.Profile.Application.Commands.CreateProfile;

public abstract record CreateProfileCommand(string FirstName, string LastName, string PhoneNumber): IRequest;