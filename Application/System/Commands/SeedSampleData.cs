using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Domain.Entities;

namespace Exam2C2P.Application.System.Commands
{
    public class SampleDataSeeder
    {
        private readonly IExamDatabaseDbContext _context;

        public SampleDataSeeder(IExamDatabaseDbContext context)
        {
            _context = context;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Transactions.Any())
            {
                return;
            }

            await SeedCustomersAsync(cancellationToken);

        }

        private async Task SeedCustomersAsync(CancellationToken cancellationToken)
        {
            var transaction = new[]
            {

                new Transaction { TransactionId ="Invoice0000001",
                    Amount = 1000,
                    TransactionDate = Convert.ToDateTime("2019-01-23T13:45:10"),
                    CurrencyCode= "USD" ,Status="D" , FileType ="xml"},

                new Transaction { TransactionId ="Invoice0000002",
                    Amount = 2000,
                    TransactionDate = Convert.ToDateTime("2019-01-23T13:45:10"),
                    CurrencyCode= "USD" ,Status="D" , FileType ="csv"},
            };

            _context.Transactions.AddRange(transaction);

            await _context.SaveChangesAsync(cancellationToken);
        }



    }
}
