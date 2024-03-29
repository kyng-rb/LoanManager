using LoanManager.Application.Common.Interfaces.Authentication;
using LoanManager.Domain.Entities;

using Moq;

namespace LoanManager.Application.Test.Authentication.Common;

public static class JwtTokenGeneratorFaker
{
    public static IJwtTokenGenerator GetMock()
    {
        var tokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        tokenGeneratorMock
            .Setup(r => r.Generate(It.IsAny<User>()))
            .Returns(Guid.NewGuid().ToString());

        return tokenGeneratorMock.Object;
    }
}