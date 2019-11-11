using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Exam2C2P.Application.Transactions.Commands
{
    public class FileUploadCommand : IRequest
    {
     //    public FormFile File { get; set; }


        public class Handler : IRequestHandler<FileUploadCommand>
        {
            private readonly IExamDatabaseDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IExamDatabaseDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(FileUploadCommand request, CancellationToken cancellationToken)
            {
                var entity = new Transaction
                {
                   
                };

                _context.Transactions.AddRange(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
