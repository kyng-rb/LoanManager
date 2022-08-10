using ErrorOr;

using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;

namespace LoanManager.Application.Tests.Authentication.Commands;

public class RegisterCommandHandlerTest
{
    private readonly RegisterCommandHandler _handler;

    public RegisterCommandHandlerTest(IJWTTokenGenerator jWtTokenGenerator,
                                      IUserRepository userRepository)
    {
        _handler = new RegisterCommandHandler(jWtTokenGenerator: jWtTokenGenerator, 
                                              userRepository: userRepository);
    }
    
    [Fact]
    public async Task Should_Register_With_Valid_Input()
    {
        //arrange
        var command = RegisterCommandFaker.Fake();

        //act
        var sut = await _handler.Handle(command, default);

        //assert
        sut.IsError.Should().BeFalse();
        sut.Value.Should().NotBeNull();
        sut.Value.Token.Should().NotBeNullOrEmpty();
        sut.Value.User.FirstName.Should().BeEquivalentTo(command.FirstName);
        sut.Value.User.LastName.Should().BeEquivalentTo(command.LastName);
        sut.Value.User.Email.Should().BeEquivalentTo(command.Email);
        sut.Value.User.Password.Should().BeEquivalentTo(command.Password);
        sut.Value.User.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Should_Fail_With_Duplicated_Email()
    {
        //arrange
        var command = RegisterCommandFaker.Fake();
        var secondCommand = RegisterCommandFaker.Fake() with { Email = command.Email };
        
        //act
        _ = await _handler.Handle(command, default);
        var sut = await _handler.Handle(secondCommand, default);
        
        //assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Should().HaveCount(1);
        sut.Errors.First().Type.Should().Be(ErrorType.Conflict);
    }
}