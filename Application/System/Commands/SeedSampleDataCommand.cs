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
        private readonly IUserManager _userManager;

        public SeedSampleDataCommandHandler(IExamDatabaseDbContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Unit> Handle(SeedSampleDataCommand request, CancellationToken cancellationToken)
        {
            var seeder = new SampleDataSeeder(_context, _userManager);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
