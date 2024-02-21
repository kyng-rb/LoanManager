using AutoFixture;
using FluentAssertions;
using LoanManager.Application.Customer.Queries.Retrieve;
using LoanManager.Application.Test.Customer.Commands.Common;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Query;

public class QueryHandlerTest 
    : BaseHandler
{
    private readonly QueryHandler _queryHandler;

    public QueryHandlerTest()
    {
        _queryHandler = new QueryHandler(_customerRepositoryMock.Object);
    }

    private void GivenCustomerRepositoryGet() =>
        _customerRepositoryMock
            .Setup(x => x.Get(It.IsAny<string>()))
            .Returns(Array.Empty<Domain.Entities.Customer>());

    private void GivenCustomerRepositoryGetWithMatches()
    {
        var customers = _fixture.Create<Domain.Entities.Customer[]>();

        _customerRepositoryMock.Setup(x => x.Get(It.IsAny<string>()))
            .Returns(customers);
    }

    private void ThenCustomerRepositoryGetWasCalled() =>
        _customerRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
    
    [Fact]
    public async Task Should_Succeed_With_Not_Matches()
    {
        // arrange
        GivenCustomerRepositoryGet();
        var queryRequest = _fixture.Create<Application.Customer.Queries.Retrieve.Query>();
        
        // act
        var sut = await _queryHandler.Handle(queryRequest, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customers.Should().BeEmpty();
        ThenCustomerRepositoryGetWasCalled();
    }
    
    [Fact]
    public async Task Should_Succeed_With_Matches()
    {
        // arrange
        GivenCustomerRepositoryGetWithMatches();
        var queryRequest = _fixture.Create<Application.Customer.Queries.Retrieve.Query>();
        
        // act
        var sut = await _queryHandler.Handle(queryRequest, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customers.Should().NotBeEmpty();
        ThenCustomerRepositoryGetWasCalled();
    }
}