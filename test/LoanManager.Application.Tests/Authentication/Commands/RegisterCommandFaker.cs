using Bogus;

using LoanManager.Application.Authentication.Commands.Register;

namespace LoanManager.Application.Tests.Authentication.Commands;

public static class RegisterCommandFaker
{
    public static Command Fake()
    {
        var faker = new Faker<Command>()
        .CustomInstantiator(faker => new Command(
            FirstName: faker.Name.FirstName(),
            LastName: faker.Person.LastName,
            Email: faker.Internet.Email(),
            Password: faker.Internet.Password()));

        return faker.Generate();
    }
}