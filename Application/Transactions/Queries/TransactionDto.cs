using Application.Common.Mappings;
using AutoMapper;
using Exam2C2P.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam2C2P.Application.Transactions.Queries
{
    public class TransactionDto : IMapFrom<Transaction>
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Transaction, TransactionDto>()
                .ForMember(x => x.Payment, opt => opt.MapFrom(s => s.Amount + " " + s.CurrencyCode));

        }

        
    }
}
