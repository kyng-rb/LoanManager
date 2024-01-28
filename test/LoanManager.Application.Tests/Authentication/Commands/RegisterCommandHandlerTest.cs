using ErrorOr;

using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Tests.Authentication.Common;
using LoanManager.Application.Tests.Common.Mocks.Persistence;
using LoanManager.Domain.Entities;

namespace LoanManager.Application.Tests.Authentication.Commands;

public class RegisterCommandHandlerTest
{
    [Fact]
    public async Task Should_Register_With_Valid_Input()
    {
        //arrange
        var tokenGenerator = JwtTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();
        var handler = new CommandHandler(jWtTokenGenerator: tokenGenerator,
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
        sut.Value.User.Id.Should().NotBe(0);
    }

    [Fact]
    public async Task Should_Fail_With_Duplicated_Email()
    {
        //arrange
        var fakeData = RegisterCommandFaker.Fake();
        var seedUser = new User
        {
            Email = fakeData.Email,
            Id = new Random().Next(),
            Password = fakeData.Password,
            FirstName = fakeData.FirstName,
            LastName = fakeData.LastName
        };

        var userRepository = new UserRepositoryMock(seedUser);
        var tokenGenerator = JwtTokenGeneratorFaker.GetMock();
        var handler = new CommandHandler(jWtTokenGenerator: tokenGenerator,
                                                 userRepository: userRepository);

        var command = RegisterCommandFaker.Fake() with { Email = fakeData.Email };

        //act
        var sut = await handler.Handle(command, default);

        //assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Should().HaveCount(1);
        sut.Errors.First().Type.Should().Be(ErrorType.Conflict);
    }
}