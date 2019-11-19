using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2C2P.Application.Dropdowns
{
    public class GetCurrencyCodeQueryHandler : IRequestHandler<GetCurrencyCodeQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetCurrencyCodeQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
             {
                 return Enum.GetValues(typeof(CurrencyCodes))
                                        .Cast<CurrencyCodes>()
                                        .Select(v => v.ToString())
                                        .ToList();
             });
          
        }
    }
}
