using FluentValidation;

namespace LoanManager.Application.Customer.Common;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> PhoneMustHaveNicaraguanFormat<T>(this IRuleBuilder<T, string> ruleBuilder) {
        return ruleBuilder.NotEmpty().WithMessage("Phone Number is required.")
            .Length(8).WithMessage("PhoneNumber must have 8 characters.")
            .Matches("[0-9]{8}$").WithMessage("PhoneNumber must have the following format ########");
    }
}