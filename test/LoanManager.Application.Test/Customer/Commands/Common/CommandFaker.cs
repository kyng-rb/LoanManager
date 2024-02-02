using Bogus;
using LoanManager.Application.Customer.Commands.Register;

namespace LoanManager.Application.Test.Customer.Commands.Common;

public static class CommandFaker
{
    public static Application.Customer.Commands.Register.Command RegisterCommand()
    {
        var faker = new Faker<Command>()
            .CustomInstantiator(faker => new Command(
                FirstName: faker.Name.FirstName(),
                LastName: faker.Person.LastName,
                Phone : faker.Phone.PhoneNumber("########")));

        return faker.Generate();
    }
    
    public static Application.Customer.Commands.Update.Command UpdateCommand()
    {
        var faker = new Faker<Application.Customer.Commands.Update.Command>()
            .CustomInstantiator(faker => new Application.Customer.Commands.Update.Command(
                CustomerId: faker.Random.Int(),
                FirstName: faker.Name.FirstName(),
                LastName: faker.Person.LastName,
                Phone : faker.Phone.PhoneNumber("########")
                ));

        return faker.Generate();
    }
}