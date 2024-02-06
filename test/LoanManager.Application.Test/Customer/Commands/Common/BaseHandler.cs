using AutoFixture;
using Bogus;
using LoanManager.Application.Common.Interfaces.Persistence;
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
    protected readonly Mock<ICustomerRepository> _customerRepositoryMock = new (MockBehavior.Strict);
    
    protected void GivenCustomerNotExistsByPhoneNumberRepository() 
        => _customerRepositoryMock
            .Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(false);
    
    protected void GivenCustomerExistsPhoneNumberRepository() 
        => _customerRepositoryMock.Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(true);
    
    protected void GivenCustomerNotFoundRepository()
    {
        _customerRepositoryMock
            .Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
    }
    
    protected void ThenExistsByPhoneWasCalled() 
        => _customerRepositoryMock.Verify(x => x.ExistsByPhone(It.IsAny<string>()), Times.Once);
    
    protected void ThenExistsByIdWasCalled() 
        => _customerRepositoryMock.Verify(x => x.ExistsById(It.IsAny<int>()), Times.Once);
}
