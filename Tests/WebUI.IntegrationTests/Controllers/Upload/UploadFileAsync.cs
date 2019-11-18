using Exam2C2P.Application.Transactions.Queries;
using Exam2C2P.WebUI.IntegrationTests.Controllers.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI;
using Xunit;


namespace Exam2C2P.WebUI.IntegrationTests.Controllers.Upload
{
    public class UploadCSVFileReturnSuccess : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UploadCSVFileReturnSuccess(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Upload_csv_file_return_success()
        {

            var expectedContentType = "text/xml; charset=utf-8";
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using (var file1 = File.OpenRead(@"Files\Test.csv"))
            using (var content1 = new StreamContent(file1))
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(content1, "file");

                var response = await client.PostAsync(url, formData);

                response.EnsureSuccessStatusCode();
                int id = await Utilities.GetResponseContent<int>(response);

                Assert.NotEqual(0, 0);
            }

        }
    }


}
