using Bogus;

using ErrorOr;

using FluentAssertions;

using LoanManager.Application.Authentication.Queries.Login;
using LoanManager.Application.Test.Authentication.Common;
using LoanManager.Application.Test.Common.Mocks.Persistence;
using LoanManager.Domain.UserAggregate;

namespace LoanManager.Application.Test.Authentication.Queries;

public class LoginQueryHandlerTest
{
    [Fact]
    public async Task Should_Fail_With_No_Existing_User()
    {
        //arrange
        var tokenGenerator = JwtTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();
        var handler = new LoginQueryHandler(jWtTokenGenerator: tokenGenerator,
                                            userRepository: userRepository);

        var loginQuery = LoginQueryFaker.Fake();

        //act
        var sut = await handler.Handle(loginQuery, default);

        //assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Should().HaveCount(1);
        sut.Errors.First().Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task Should_Fail_With_Wrong_Password()
    {
        //arrange
        var tokenGenerator = JwtTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();
        var faker = new Faker();

        var loginQuery = LoginQueryFaker.Fake();
        var user = new User
        {
            Id = default,
            FirstName = faker.Name.FirstName(),
            LastName = faker.Name.LastName(),
            Email = loginQuery.Email,
            Password = faker.Internet.Password()
        };

        userRepository.Add(user);

        var handler = new LoginQueryHandler(jWtTokenGenerator: tokenGenerator,
                                            userRepository: userRepository);
        //act
        var sut = await handler.Handle(loginQuery, default);

        //assert
        sut.IsError.Should().BeTrue();
        sut.Errors.Should().HaveCount(1);
        sut.Errors.First().Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task Should_Login_With_Valid_Credentials()
    {
        //arrange
        var tokenGenerator = JwtTokenGeneratorFaker.GetMock();
        var userRepository = new UserRepositoryMock();

        var faker = new Faker();

        var loginQuery = LoginQueryFaker.Fake();
        var user = new User
        {
            Id = default,
            FirstName = faker.Name.FirstName(),
            LastName = faker.Name.LastName(),
            Email = loginQuery.Email,
            Password = loginQuery.Password
        };

        userRepository.Add(user);

        var handler = new LoginQueryHandler(jWtTokenGenerator: tokenGenerator,
                                            userRepository: userRepository);
        //act
        var sut = await handler.Handle(loginQuery, default);

        //assert
        sut.IsError.Should().BeFalse();
        sut.Value.Token.Should().NotBeEmpty();
        sut.Value.User.Should().Be(user);
    }
}