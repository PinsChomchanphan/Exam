using Exam2C2P.Domain.Common;
using System;

namespace Exam2C2P.Domain.Entities
{
    public class Transaction : AuditableEntity
    {
        public int Id { get; set; }
        public string TransactionId  { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode  { get; set; }
        public DateTime TransactionDate  { get; set; }
        public string Status { get; set; }
        public string FileType { get; set; }

    }
}
