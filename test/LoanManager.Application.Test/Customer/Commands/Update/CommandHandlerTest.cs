using AutoFixture;
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
        => _customerRepositoryMock.Setup(x => x.ExistsById(It.IsAny<int>()))
            .Returns(true);

    private void GivenCustomerUpdateRepository()
        => _customerRepositoryMock.Setup(x => x.Update(It.IsAny<Domain.Entities.Customer>()));
    
    private void ThenCustomerUpdateWasCalled()
        => _customerRepositoryMock.Verify(x => x.Update(It.IsAny<Domain.Entities.Customer>()), Times.Once);

    [Fact]
    public async Task Should_Fail_With_Customer_Not_Found()
    {
        // arrange
        GivenCustomerNotFoundRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.NotFound);
        ThenExistsByIdWasCalled();
    }
    
    [Fact]
    public async Task Should_Fail_With_Found_Customer_And_Already_Used_Phone()
    {
        // arrange
        GivenCustomerExistsPhoneNumberRepository();
        GivenCustomerFoundRepository();
        var command = _fixture.Create<Command>();
        
        // act
        var sut = await _handler.Handle(command, default);

        // assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Count.Should().Be(1);
        sut.Errors.First().Should().Be(Errors.Customer.DuplicatedPhone);
        ThenExistsByIdWasCalled();
        ThenExistsByPhoneWasCalled();
    }
    
    [Fact]
    public async Task Should_Succeed_With_Found_User_And_Not_Used_PhoneNumber()
    {
        // arrange
        GivenCustomerFoundRepository();
        GivenCustomerNotExistsByPhoneNumberRepository();
        GivenCustomerUpdateRepository();
        var command = _fixture.Create<Command>();

        // act
        var sut = await _handler.Handle(command, default);

        //assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customer.Should().NotBeNull()
            .And.BeEquivalentTo(command, option
                => option.WithMapping<Domain.Entities.Customer>(
                    s => s.CustomerId,
                    x => x.Id));
        
        ThenExistsByIdWasCalled();
        ThenExistsByPhoneWasCalled();
        ThenCustomerUpdateWasCalled();
    }
}