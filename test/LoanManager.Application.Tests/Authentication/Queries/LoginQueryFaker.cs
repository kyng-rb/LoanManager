using Bogus;

using LoanManager.Application.Authentication.Queries.Login;

namespace LoanManager.Application.Tests.Authentication.Queries;

public static class LoginQueryFaker
{
    public static LoginQuery Fake()
    {
        var faker = new Faker<LoginQuery>()
            .CustomInstantiator(faker =>
                new LoginQuery(Email: faker.Internet.Email(), Password: faker.Internet.Password()));

        return faker.Generate();
    }
}