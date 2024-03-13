using Ardalis.Specification;
using ErrorOr;
using LoanManager.Application.Customer.Common;
using LoanManager.Domain.Common.Errors;
using MediatR;

namespace LoanManager.Application.Customer.Commands.Register;

public class CommandHandler(IRepositoryBase<Domain.CustomerAggregate.Customer> repository)
    : IRequestHandler<Command, ErrorOr<CommandResult>>
{
    private readonly IRepositoryBase<Domain.CustomerAggregate.Customer> _repository = repository;

    public async Task<ErrorOr<CommandResult>> Handle(Command request, CancellationToken cancellationToken)
    {
        var existsByPhoneSpec = new Domain.CustomerAggregate.Specifications.CustomerByPhoneNumber(request.Phone);
        if (await _repository.AnyAsync(existsByPhoneSpec, cancellationToken))
            return Errors.Customer.DuplicatedPhone;
        
        var customer = new Domain.CustomerAggregate.Customer()
        {
            Phone = request.Phone,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        
        await _repository.AddAsync(customer, cancellationToken);
        var result = new CommandResult(Common.Customer.FromEntity(customer));
        
        return result;
    }
}