using Exam2C2P.Domain.Entities;
using Exam2C2P.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Exam2C2P.Application.UnitTests.Common
{
    class ExamDatabaseContextFactory
    {
        public static ExamDatabaseDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ExamDatabaseDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ExamDatabaseDbContext(options);

            context.Database.EnsureCreated();

            context.Transactions.AddRange(new[] {
                new Transaction { TransactionId ="Invoice0000001",
                    Amount = 1000,
                    TransactionDate = Convert.ToDateTime("2019-01-23T13:45:10"),
                    CurrencyCode= "USD" ,Status="D" , FileType ="xml"},

                new Transaction { TransactionId ="Invoice0000002",
                    Amount = 2000,
                    TransactionDate = Convert.ToDateTime("2019-01-23T13:45:10"),
                    CurrencyCode= "USD" ,Status="D" , FileType ="xml"},
            });


            context.SaveChanges();

            return context;
        }

        public static void Destroy(ExamDatabaseDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }


}
