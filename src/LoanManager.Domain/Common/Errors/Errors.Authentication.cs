using ErrorOr;

namespace LoanManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
        code: "Invalid:Credentials",
        description: "User invalid credentials.");
    }
}