using LoanManager.Application.Transactions.Common;

namespace LoanManager.Application.Transactions.Queries.Loans;

public record LoansQueryResult(IEnumerable<LoanTransactionResult> Loans, int Total);