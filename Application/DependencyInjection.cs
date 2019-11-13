using System.Reflection;
using Exam2C2P.Application.Common.Behaviours;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Exam2C2P.Application.Common.Logger;
using Microsoft.Extensions.Configuration;

namespace Exam2C2P.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddLogging(logging =>
            {               
                logging.ClearProviders();
                logging.AddProvider(new LoggerDatabaseProvider(configuration.GetConnectionString("ExamDatabase")));
                // logging.AddConsole();

            }).Configure<LoggerFilterOptions>(options => options.MinLevel =
                                              LogLevel.Information);

            return services;
        }
    }
}
