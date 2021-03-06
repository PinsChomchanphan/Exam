﻿using Exam2C2P.Application.Transactions.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : BaseController
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> UploadFileAsync([FromForm] IFormFile file)
        {
            try
            {
                var supportedTypes = new[] { "csv", "xml"};
                int filesize = 1 * 1024 * 1024; // bytes
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return BadRequest("Unknown format");
                }
                else if (file.Length > filesize)
                {
                    return BadRequest("File size Should Be UpTo 1MB");
                }
                else
                {
                    await Mediator.Send(new FileUploadCommand()
                    {
                        FileStream = file.OpenReadStream(),
                        FileType = fileExt
                    });
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}