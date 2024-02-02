using FluentAssertions;
using LoanManager.Application.Customer.Commands.Update;
using LoanManager.Application.Test.Customer.Commands.Common;
using LoanManager.Domain.Common.Errors;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Update;

public class CommandHandlerTest : BaseHandler
{
    private readonly CommandHandler _handler;
    
    public CommandHandlerTest()
    {
        _handler = new CommandHandler(_customerRepositoryMock.Object);
    }

    private void GivenCustomerFoundRepository()
    {
        var customer = new Domain.Entities.Customer()
        {
            Phone = _faker.Phone.PhoneNumber(),
            FirstName = _faker.Name.FirstName()
        };
        
        _customerRepositoryMock
            .Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(customer);
    }

    [Fact]
    public async Task Should_Fail_With_Already_Used_Phone()
    {
        // arrange
        GivenCustomerAlreadyUsedPhoneRepository();
        GivenCustomerFoundRepository();
        var command = CommandFaker.UpdateCommand();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.DuplicatedPhone);
        ThenGetByIdWasCalled();
        ThenExistsByPhoneWasCalled();
    }
    
    [Fact]
    public async Task Should_Fail_With_Customer_Not_Found()
    {
        // arrange
        GivenCustomerNotFoundRepository();
        var command = CommandFaker.UpdateCommand();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.NotFound);
        ThenGetByIdWasCalled();
    }
}