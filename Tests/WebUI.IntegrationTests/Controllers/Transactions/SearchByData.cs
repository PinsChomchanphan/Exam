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
   
    public class SearchByData : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public SearchByData(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SearchByDataReturnsTransactionDtoList()
        {
            var client = _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/transaction/search");

            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<List<TransactionDto>>(response);

            Assert.IsType<List<TransactionDto>>(vm);
        }
    }
}
