using Bogus;

using ErrorOr;

using FluentAssertions;

using LoanManager.Application.Authentication.Commands.Register;
using LoanManager.Application.Authentication.Queries.Login;
using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Tests.Authentication.Common;
using LoanManager.Application.Tests.Common.Mocks.Persistence;
using LoanManager.Domain.Entities;

namespace LoanManager.Application.Tests.Authentication.Queries;

public class LoginQueryHandlerTest
{
    [Fact]
    public async Task Should_Fail_With_No_Existing_User()
    {
        //arrange
        var tokenGenerator = JWTTokenGeneratorFaker.GetMock();
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
        var tokenGenerator = JWTTokenGeneratorFaker.GetMock();
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
        var tokenGenerator = JWTTokenGeneratorFaker.GetMock();
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