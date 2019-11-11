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
        private readonly IUserManager _userManager;

        public SampleDataSeeder(IExamDatabaseDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
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
                
                new Transaction { }
            };

            _context.Transactions.AddRange(transaction);

            await _context.SaveChangesAsync(cancellationToken);
        }



    }
}
