using AutoFixture;
using FluentAssertions;
using LoanManager.Application.Customer.Queries.Retrieve;
using LoanManager.Application.Test.Customer.Commands.Common;
using LoanManager.Domain.CustomerAggregate.Specifications;
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

    private void Given_Empty_CustomerRepository() =>
        _customerRepositoryMock
            .Setup(x => x.ListAsync(It.IsAny<CustomerSearch>(), default))
            .ReturnsAsync(Array.Empty<Domain.CustomerAggregate.Customer>().ToList());

    private void Given_With_Matches_CustomerRepository()
    {
        var customers = _fixture.Create<List<Domain.CustomerAggregate.Customer>>();

        _customerRepositoryMock.Setup(x => x.ListAsync(It.IsAny<CustomerSearch>(), default))
            .ReturnsAsync(customers);
    }

    private void Then_List_CustomerRepository_Was_Called() =>
        _customerRepositoryMock.Verify(x => x.ListAsync(It.IsAny<CustomerSearch>(), default), Times.Once);
    
    [Fact]
    public async Task Should_Succeed_With_Not_Matches()
    {
        // arrange
        Given_Empty_CustomerRepository();
        var queryRequest = _fixture.Create<Application.Customer.Queries.Retrieve.Query>();
        
        // act
        var sut = await _queryHandler.Handle(queryRequest, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customers.Should().BeEmpty();
        Then_List_CustomerRepository_Was_Called();
    }
    
    [Fact]
    public async Task Should_Succeed_With_Matches()
    {
        // arrange
        Given_With_Matches_CustomerRepository();
        var queryRequest = _fixture.Create<Application.Customer.Queries.Retrieve.Query>();
        
        // act
        var sut = await _queryHandler.Handle(queryRequest, default);

        // assert
        sut.IsError.Should().BeFalse();
        sut.Value.Customers.Should().NotBeEmpty();
        Then_List_CustomerRepository_Was_Called();
    }
}