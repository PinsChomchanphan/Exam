using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam2C2P.Application.Dropdowns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : BaseController
    {
        [HttpGet("currencyCode")]
        [AllowAnonymous]
        public async Task<ActionResult<List<string>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCurrencyCodeQuery()));
        }

    }
}