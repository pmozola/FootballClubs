using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;

namespace FootballClubs.Auth;

public class JwtKeyService(IOptions<AppAuthorizationOptions> options) : IJwtKeyService
{
    public byte[] GetKey() =>
        Encoding.ASCII.GetBytes(options.Value.SecurityKey);
}

public interface IJwtKeyService
{
    byte[] GetKey();
}

public class AppAuthorizationOptions
{
    public static readonly string SectionName = "authorization";
    public string SecurityKey { get; private set; } = string.Empty;
}

public class AppAuthorizationOptionsValidation: IValidateOptions<AppAuthorizationOptions>
{
    public ValidateOptionsResult Validate(string? name, AppAuthorizationOptions options)
    {
        return string.IsNullOrEmpty(options.SecurityKey) ? 
            ValidateOptionsResult.Fail("SecurityKey is required") :
            ValidateOptionsResult.Success;
    }
}