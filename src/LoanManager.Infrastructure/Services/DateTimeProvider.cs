using LoanManager.Application.Common.Interfaces.Services;

namespace LoanManager.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    DateTime IDateTimeProvider.UtcNow => DateTime.UtcNow;
}