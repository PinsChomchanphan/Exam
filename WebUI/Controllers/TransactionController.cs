using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam2C2P.Application.Transactions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<TransactionDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetTransactionQuery()));
        }

        [HttpPost("search")]
        [AllowAnonymous]
        public async Task<ActionResult<List<TransactionDto>>> SearchByData(SearchTransactionQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}