using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Tests.Common.Mocks.Authentication;
using LoanManager.Application.Tests.Common.Mocks.Persistence;

using Microsoft.Extensions.DependencyInjection;

namespace LoanManager.Application.Tests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepositoryMock>();
        services.AddSingleton(JWTTokenGeneratorMock.GetMock());
    }
}