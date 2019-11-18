using System.Reflection;
using Exam2C2P.Application.Common.Behaviours;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Exam2C2P.Application.Common.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Persistence;
using System;

namespace Exam2C2P.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIoC(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("ExamDatabase");
            #region Persistence

            services.AddDbContext<ExamDatabaseDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IExamDatabaseDbContext>(provider => provider.GetService<ExamDatabaseDbContext>());

            #endregion

            #region Application layer
            var assembly = AppDomain.CurrentDomain.Load("Exam2C2P.Application");
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddProvider(new LoggerDatabaseProvider(connectionString));
                // logging.AddConsole();

            }).Configure<LoggerFilterOptions>(options => options.MinLevel =
                                              LogLevel.Information);
            #endregion



            return services;
        }
    }
}

