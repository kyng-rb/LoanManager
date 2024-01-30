using Bogus;
using LoanManager.Application.Customer.Commands.Register;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public static class CommandFaker
{
    public static Command Command()
    {
        var faker = new Faker<Command>()
            .CustomInstantiator(faker => new Command(
                FirstName: faker.Name.FirstName(),
                LastName: faker.Person.LastName,
                Phone : faker.Phone.PhoneNumber("########")));

        return faker.Generate();
    }
}