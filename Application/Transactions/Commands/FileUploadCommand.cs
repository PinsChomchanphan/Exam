using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Application.Helper;
using Exam2C2P.Domain.Entities;
using MediatR;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Exam2C2P.Application.Transactions.Commands
{
    public class FileUploadCommand : IRequest<int>
    {
        public Stream FileStream { get; set; }
        public string FileType { get; set; }
        public class FileUploadCommandHandler : IRequestHandler<FileUploadCommand, int>
        {
            private readonly IExamDatabaseDbContext _context;
            private readonly IMediator _mediator;

            public FileUploadCommandHandler(IExamDatabaseDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<int> Handle(FileUploadCommand request, CancellationToken cancellationToken)
            {


                //_context.Transactions.AddRange(entity);
                var xml = new TransactionXmlReader();
                var res = xml.Read(request.FileStream);
                //await _context.SaveChangesAsync(cancellationToken);
                return 0;
            }
        }
    }
}
