namespace FootballClubs.Profile.Domain;

public class UserProfile
{
    public required string PhoneNumber { get; set; }
    public required string LastName { get; init; }
    public required string FirstName { get; init; }


    private UserProfile()
    {
    }

    public static UserProfile Create(string firstName, string lastName, string phoneNumber) => new()
    {
        FirstName = firstName,
        LastName = lastName,
        PhoneNumber = phoneNumber
    };
}