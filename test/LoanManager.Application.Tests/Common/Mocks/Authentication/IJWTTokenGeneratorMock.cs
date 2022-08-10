using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Domain.Entities;

using Moq;

namespace LoanManager.Application.Tests.Common.Mocks.Authentication;

public class JWTTokenGeneratorMock
{
    public static IJWTTokenGenerator GetMock()
    {
        var tokenGeneratorMock = new Mock<IJWTTokenGenerator>();
        tokenGeneratorMock
            .Setup(r => r.Generate(It.IsAny<User>()))
            .Returns(Guid.NewGuid().ToString());

        return tokenGeneratorMock.Object;
    }
}