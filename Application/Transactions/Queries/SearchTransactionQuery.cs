using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2C2P.Application.Transactions.Queries
{
    public class SearchTransactionQuery : IRequest<IEnumerable<TransactionDto>>
    {
    }
}
