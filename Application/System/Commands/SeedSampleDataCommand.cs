using MediatR;
using Exam2C2P.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2C2P.Application.System.Commands
{
    public class SeedSampleDataCommand : IRequest
    {
    }

    public class SeedSampleDataCommandHandler : IRequestHandler<SeedSampleDataCommand>
    {
        private readonly IExamDatabaseDbContext _context;

        public SeedSampleDataCommandHandler(IExamDatabaseDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
