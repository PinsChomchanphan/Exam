using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exam2C2P.Domain.Entities;
using Exam2C2P.Domain.Common;
using System;
using Exam2C2P.Application.Common.Interfaces;

namespace Exam2C2P.Persistence
{
    public class ExamDatabaseDbContext : DbContext, IExamDatabaseDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ExamDatabaseDbContext(DbContextOptions<ExamDatabaseDbContext> options)
            : base(options)
        {
        }

        public ExamDatabaseDbContext(
            DbContextOptions<ExamDatabaseDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<Transaction> Transactions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExamDatabaseDbContext).Assembly);
        }
    }
}
