using AutoMapper;
using Exam2C2P.Application.Transactions.Queries;
using Exam2C2P.Application.UnitTests.Common;
using Exam2C2P.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Shouldly;
using System;

namespace Exam2C2P.Application.UnitTests.Transactions.Queries
{
    [Collection("QueryCollection")]
    public class GetTransactionQueryHandlerTest : CommandTestBase
    {
        private readonly ExamDatabaseDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTransactionQueryHandlerTest(QueryTestFixture fixture)
        {
            _dbContext = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetCustomerDetail()
        {
            var sut = new GetTransactionQueryHandler(_dbContext, _mapper);

            var result = await sut.Handle(new GetTransactionQuery() , CancellationToken.None);
            result.ShouldContain(m => m.TransactionId.Equals("Invoice0000001", StringComparison.CurrentCultureIgnoreCase));

        }
    }

   
}
