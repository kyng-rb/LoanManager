using ErrorOr;

namespace LoanManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error DuplicatedPhone => Error.Conflict(code: "Phone.Duplicated", description: "Phone is already in use");
        public static Error NotFound => Error.Conflict(code: "Customer.NotFound", description: "Customer record not found");
    }
}