using ErrorOr;

namespace LoanManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "Email.Duplicated", description: "Email is already in use");
    }
}