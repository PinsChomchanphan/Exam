using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Mappings;
using AutoMapper;
using Exam2C2P.Persistence;
using Xunit;

namespace Exam2C2P.Application.UnitTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ExamDatabaseDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = ExamDatabaseContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ExamDatabaseContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
