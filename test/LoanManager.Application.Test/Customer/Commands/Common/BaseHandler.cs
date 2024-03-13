using Ardalis.Specification;
using AutoFixture;
using Bogus;
using LoanManager.Domain.CustomerAggregate.Specifications;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Common;

public abstract class BaseHandler
{
    
    protected const string InvalidPhoneNumberLengthMessage = "PhoneNumber must have 8 characters.";
    protected const string RequiredFirstNameMessage = "FirstName is required.";
    protected const string InvalidPhoneNumberFormatMessage = "PhoneNumber must have the following format ########";
    protected const string RequiredPhoneNumberMessage = "Phone Number is required.";
    protected const string InvalidIdentifierMessage = "Invalid Customer Id value";
    
    protected readonly Faker _faker = new ();
    protected readonly Fixture _fixture = new();
    protected readonly Mock<IRepositoryBase<Domain.CustomerAggregate.Customer>> _customerRepositoryMock = new (MockBehavior.Strict);
    
    protected void Given_Not_Found_By_PhoneNumber_CustomerRepository() 
        => _customerRepositoryMock
            .Setup(x => x.AnyAsync(It.IsAny<CustomerByPhoneNumber>(), default)).ReturnsAsync(false);
    
    protected void Given_Found_By_PhoneNumber_CustomerRepository()
        => _customerRepositoryMock
        .Setup(x => x.AnyAsync(It.IsAny<CustomerByPhoneNumber>(), default)).ReturnsAsync(true);
    
    protected void Given_Not_Found_By_Id_CustomerRepository()
        => _customerRepositoryMock
            .Setup(x => x.AnyAsync(It.IsAny<CustomerById>(), default)).ReturnsAsync(false);
    
    protected void Then_Any_By_Phone_Was_Called() 
        => _customerRepositoryMock.Verify(x => x.AnyAsync(It.IsAny<CustomerByPhoneNumber>(), default), Times.Once);
    
    protected void Then_Any_By_Id_Was_Called() 
        => _customerRepositoryMock.Verify(x => x.AnyAsync(It.IsAny<CustomerById>(), default), Times.Once);
}
