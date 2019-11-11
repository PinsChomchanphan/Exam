using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Exam2C2P.Application.Common.Interfaces;


namespace Exam2C2P.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExamDatabaseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ExamDatabase")));

            services.AddScoped<IExamDatabaseDbContext>(provider => provider.GetService<ExamDatabaseDbContext>());

            return services;
        }
    }
}
