using ErrorOr;

using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Tests.Authentication.Common;
using LoanManager.Application.Tests.Common.Mocks.Persistence;

namespace LoanManager.Application.Tests.Authentication.Commands;

public class RegisterCommandHandlerTest
{
    [Fact]
    public async Task Should_Register_With_Valid_Input()
    {
        //arrange
        var tokenGenerator = JWTTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();
        var handler = new RegisterCommandHandler(jWtTokenGenerator: tokenGenerator, 
                                                 userRepository: userRepository);
        
        var command = RegisterCommandFaker.Fake();

        //act
        var sut = await handler.Handle(command, default);

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
        
        var tokenGenerator = JWTTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();
        var handler = new RegisterCommandHandler(jWtTokenGenerator: tokenGenerator, 
                                                 userRepository: userRepository);
        
        var command = RegisterCommandFaker.Fake();
        var secondCommand = RegisterCommandFaker.Fake() with { Email = command.Email };
        
        //act
        _ = await handler.Handle(command, default);
        var sut = await handler.Handle(secondCommand, default);
        
        //assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Should().HaveCount(1);
        sut.Errors.First().Type.Should().Be(ErrorType.Conflict);
    }
}