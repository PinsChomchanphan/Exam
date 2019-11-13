using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Exam2C2P.Domain.Entities;
using Exam2C2P.Persistence;
using System;

namespace Exam2C2P.WebUI.IntegrationTests.Controllers.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(ExamDatabaseDbContext context)
        {
            context.Transactions.Add(new Transaction
            {
                TransactionId = "Invoice0000001",
                Amount = 1000,
                TransactionDate = Convert.ToDateTime("2019-01-23T13:45:10"),
                CurrencyCode = "USD",
                Status = "D",
                FileType = "xml"
            });
            context.SaveChanges();
        }
    }
}
