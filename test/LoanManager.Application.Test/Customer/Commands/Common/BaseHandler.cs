using Bogus;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Domain.Common.Errors;
using Moq;

namespace LoanManager.Application.Test.Customer.Commands.Common;

public abstract class BaseHandler
{
    protected readonly Faker _faker = new ();
    protected readonly Mock<ICustomerRepository> _customerRepositoryMock = new (MockBehavior.Strict);
    
    protected void GivenCustomerAlreadyUsedPhoneRepository() 
        => _customerRepositoryMock.Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(true);
    
    protected void GivenCustomerEmptyRepository()
    {
        _customerRepositoryMock
            .Setup(x => x.ExistsByPhone(It.IsAny<string>())).Returns(false);

        _customerRepositoryMock
            .Setup(x => x.Add(It.IsAny<Domain.Entities.Customer>()));
    }

    protected void GivenCustomerNotFoundRepository()
    {
        _customerRepositoryMock
            .Setup(x => x.GetById(It.IsAny<int>())).Returns(Errors.Customer.NotFound);
    }
    
    protected void ThenExistsByPhoneWasCalled() 
        => _customerRepositoryMock.Verify(x => x.ExistsByPhone(It.IsAny<string>()), Times.Once);
    
    protected void ThenGetByIdWasCalled() 
        => _customerRepositoryMock.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
}
