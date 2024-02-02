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

    private void GivenAlreadyUsedPhoneRepository()=> _customerRepositoryMock.Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(true);

    private void GivenEmptyRepository()
    {
        var customerNotFoundResponse = Errors.Customer.NotFound;
        _customerRepositoryMock
            .Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(false);

        _customerRepositoryMock
            .Setup(x => x.Add(It.IsAny<Domain.Entities.Customer>()));
    }

    [Fact]
    public async Task Should_Fail_With_Duplicated_Phone()
    {
        // arrange
        GivenAlreadyUsedPhoneRepository();
        var command = CommandFaker.Command();
        
        // act
        var sut = await _handler.Handle(command, default);
        
        // assert
        sut.IsError.Should().BeTrue();
        sut.FirstError.Code.Should().Be(Errors.Customer.DuplicatedPhone.Code);
        sut.FirstError.Description.Should().Be(Errors.Customer.DuplicatedPhone.Description);
        _customerRepositoryMock.Verify(x => x.ExistsByPhone(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Should_Succeed_With_Valid_Input()
    {
        // arrange
        GivenEmptyRepository();
        var command = CommandFaker.Command();

        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customer.Should().NotBeNull()
            .And.BeEquivalentTo(command);
        _customerRepositoryMock.Verify(x => x.ExistsByPhone(It.IsAny<string>()), Times.Once);
        _customerRepositoryMock.Verify(x => x.Add(It.IsAny<Domain.Entities.Customer>()), Times.Once);
    }
}