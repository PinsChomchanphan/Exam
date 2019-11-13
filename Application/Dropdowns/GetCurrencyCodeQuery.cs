using MediatR;
using System.Collections.Generic;

namespace Exam2C2P.Application.Dropdowns
{

    public class GetCurrencyCodeQuery : IRequest<List<string>>
    {
    }
}
