using Exam2C2P.Application.Transactions.Queries;
using Exam2C2P.WebUI.IntegrationTests.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI;
using Xunit;

namespace Exam2C2P.WebUI.IntegrationTests.Controllers.Transactions
{
    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAll(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsTransactionDtoList()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/transaction");

            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<List<TransactionDto>>(response);

            Assert.IsType<List<TransactionDto>>(vm);
        }
    }
}
