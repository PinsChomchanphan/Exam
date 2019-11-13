using Exam2C2P.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2C2P.Application.Common.Interfaces
{
    public interface IExamDatabaseDbContext
    {
        DbSet<Transaction> Transactions { get; set; }

        DbSet<EventLog> EventLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
