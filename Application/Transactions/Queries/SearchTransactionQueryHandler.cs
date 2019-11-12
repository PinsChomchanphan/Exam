using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam2C2P.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2C2P.Application.Transactions.Queries
{

    public class SearchTransactionQueryHandler : IRequestHandler<SearchTransactionQuery, List<TransactionDto>>
    {
        private readonly IExamDatabaseDbContext _context;
        private readonly IMapper _mapper;
        public SearchTransactionQueryHandler(IExamDatabaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TransactionDto>> Handle(SearchTransactionQuery request, CancellationToken cancellationToken)
        {
            var query = (from t in _context.Transactions
                        orderby t.Created descending
                        select t).AsQueryable(); 

            if (!string.IsNullOrEmpty(request.CurrencyCode))
            {
                query = query.Where(x => x.CurrencyCode == request.CurrencyCode);
            }

            if (!string.IsNullOrEmpty(request.Status))
            {
                query = query.Where(x => x.Status == request.Status);
            }

            if (request.StartDate != null)
            {
                query = query.Where(x => x.TransactionDate >= request.StartDate);
            }

            if (request.DueDate != null)
            {
                query = query.Where(x => x.TransactionDate <= request.DueDate);
            }
            var res = await query.ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return  res;
        }
    }
}
