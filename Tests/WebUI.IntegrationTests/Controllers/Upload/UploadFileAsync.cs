using Exam2C2P.Application.Transactions.Queries;
using Exam2C2P.WebUI.IntegrationTests.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        public async Task Upload_CsvFileReturnSuccess()
        {
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using var file1 = File.OpenRead(@"Files\Test.csv");
            using var content1 = new StreamContent(file1);
            using var formData = new MultipartFormDataContent
            {
                { content1, "file", "Test.csv" }
            };

            var response = await client.PostAsync(url, formData);

            response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task Upload_XmlFileReturnSuccess()
        {
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using var file1 = File.OpenRead(@"Files\Test.xml");
            using var content1 = new StreamContent(file1);
            using var formData = new MultipartFormDataContent
            {
                { content1, "file", "Test.xml" }
            };

            var response = await client.PostAsync(url, formData);

            response.EnsureSuccessStatusCode();

            Assert.Equal("OK", response.StatusCode.ToString());
        }
        [Fact]
        public async Task Upload_TxtFileReturnFailure()
        {
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using var file1 = File.OpenRead(@"Files\Test.txt");
            using var content1 = new StreamContent(file1);
            using var formData = new MultipartFormDataContent
            {
                { content1, "file", "Test.txt" }
            };

            var response = await client.PostAsync(url, formData);
            string message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Unknown format", message.Replace("\"", ""));
        }

        [Fact]
        public async Task Upload_FileOverOneMBReturnFailure()
        {
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using var file1 = File.OpenRead(@"Files\Over1mb.csv");
            using var content1 = new StreamContent(file1);
            using var formData = new MultipartFormDataContent
            {
                { content1, "file", "Over1mb.csv" }
            };

            var response = await client.PostAsync(url, formData);
            string message = await response.Content.ReadAsStringAsync();
            Assert.Equal("File size Should Be UpTo 1MB", message.Replace("\"", ""));
        }

        [Fact]
        public async Task Upload_FileReturnFailure()
        {
            var url = "/api/upload";
            var client = _factory.GetAnonymousClient();

            using var file1 = File.OpenRead(@"Files\Test.xml");
            using var content1 = new StreamContent(file1);
            using var formData = new MultipartFormDataContent
            {
                { content1, "file" }
            };

            var response = await client.PostAsync(url, formData);


            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }
    }
}
