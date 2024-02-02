using ErrorOr;
using LoanManager.Application.Common.Interfaces.Persistence;
using LoanManager.Application.Customer.Common;
using LoanManager.Domain.Common.Errors;
using MediatR;

namespace LoanManager.Application.Customer.Commands.Update;

public class CommandHandler : IRequestHandler<Command, ErrorOr<CommandResult>>
{
    private readonly ICustomerRepository _repository;

    public CommandHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<CommandResult>> Handle(Command request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (_repository.ExistsById(request.CustomerId) is false)
            return Errors.Customer.NotFound;
        
        if (_repository.ExistsByPhone(request.Phone))
            return Errors.Customer.DuplicatedPhone;

        var customer = new Domain.Entities.Customer()
        {
            Id = request.CustomerId,
            Phone    = request.Phone,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        
        _repository.Update(customer);

        var result = new CommandResult(customer);
        return result;
    }
}