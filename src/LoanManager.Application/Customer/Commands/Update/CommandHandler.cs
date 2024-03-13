using Ardalis.Specification;
using ErrorOr;
using LoanManager.Application.Customer.Common;
using LoanManager.Domain.Common.Errors;
using LoanManager.Domain.CustomerAggregate.Specifications;
using MediatR;

namespace LoanManager.Application.Customer.Commands.Update;

public class CommandHandler(IRepositoryBase<Domain.CustomerAggregate.Customer> repository)
    : IRequestHandler<Command, ErrorOr<CommandResult>>
{
    private readonly IRepositoryBase<Domain.CustomerAggregate.Customer> _repository = repository;

    public async Task<ErrorOr<CommandResult>> Handle(Command request, CancellationToken cancellationToken)
    {
        var customerByIdSpecification = new CustomerById(request.CustomerId);
        if (!await _repository.AnyAsync(customerByIdSpecification, cancellationToken))
            return Errors.Customer.NotFound;
        
        var customerByPhoneSpecification = new CustomerByPhoneNumber(request.Phone);
        if (await _repository.AnyAsync(customerByPhoneSpecification, cancellationToken))
            return Errors.Customer.DuplicatedPhone;

        var customer = new Domain.CustomerAggregate.Customer()
        {
            Phone    = request.Phone,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Id = request.CustomerId
        };

        await _repository.UpdateAsync(customer, cancellationToken);
        
        var result = new CommandResult(Common.Customer.FromEntity(customer));
        return result;
    }
}