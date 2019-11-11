using Microsoft.EntityFrameworkCore;

namespace Exam2C2P.Persistence
{
    public class ExamDatabaseDbContextFactory : DesignTimeDbContextFactoryBase<ExamDatabaseDbContext>
    {
        protected override ExamDatabaseDbContext CreateNewInstance(DbContextOptions<ExamDatabaseDbContext> options)
        {
            return new ExamDatabaseDbContext(options);
        }
    }
}
