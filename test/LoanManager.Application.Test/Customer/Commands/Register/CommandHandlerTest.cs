using Bogus;
using FluentAssertions;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Customer.Commands.Register;
using LoanManager.Domain.Common.Errors;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Register;

public class CommandHandlerTest
{
    private readonly Faker _faker = new ();
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly CommandHandler _handler;

    public CommandHandlerTest()
    {
        _customerRepositoryMock = new(MockBehavior.Strict);  
        _handler = new CommandHandler(_customerRepositoryMock.Object);
    }

    private void RepositoryWithAlreadyUsedPhone()
    {
        var existingCustomer = new Domain.Entities.Customer()
        {
            Phone = _faker.Phone.PhoneNumber(),
            FirstName = _faker.Name.FirstName(),
            LastName = _faker.Name.LastName()
        };
        
        _customerRepositoryMock.Setup(x => x.GetByPhone(It.IsAny<string>())).Returns(existingCustomer);
    }

    [Fact]
    public async Task Should_Fail_With_Duplicated_Phone()
    {
        // arrange
        RepositoryWithAlreadyUsedPhone();
        var command = CommandFaker.Command();
        
        // act
        var sut = await _handler.Handle(command, default);
        
        // assert
        sut.IsError.Should().BeTrue();
        sut.FirstError.Code.Should().Be(Errors.Customer.DuplicatedPhone.Code);
        sut.FirstError.Description.Should().Be(Errors.Customer.DuplicatedPhone.Description);
        _customerRepositoryMock.Verify(x => x.GetByPhone(It.IsAny<string>()), Times.Once);
    }
}