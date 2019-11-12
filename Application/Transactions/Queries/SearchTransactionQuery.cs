using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2C2P.Application.Transactions.Queries
{
    public class SearchTransactionQuery : IRequest<List<TransactionDto>>
    {
        public string CurrencyCode { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
