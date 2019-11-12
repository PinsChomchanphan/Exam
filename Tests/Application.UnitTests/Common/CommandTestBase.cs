using Exam2C2P.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2C2P.Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly ExamDatabaseDbContext _context;

        public CommandTestBase()
        {
            
               _context = ExamDatabaseContextFactory.Create();
        }

        public void Dispose()
        {
            ExamDatabaseContextFactory.Destroy(_context);
        }
    }
}
