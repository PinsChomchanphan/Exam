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

    public class SearchTransactionQueryHandler : IRequestHandler<GetTransactionQuery, IEnumerable<TransactionDto>>
    {
        private readonly IExamDatabaseDbContext _context;
        private readonly IMapper _mapper;
        public SearchTransactionQueryHandler(IExamDatabaseDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {

            var res = await _context.Transactions
                    .OrderByDescending(x => x.CreatedBy)
                    .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return res;
        }
    }
}
