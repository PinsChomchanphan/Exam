using Exam2C2P.Application.Transactions.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace WebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UploadController : BaseController
    {
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> UploadFileAsync()
        {
            try
            {
                var file = Request.Form.Files[0];
                var supportedTypes = new[] { "csv", "xml"};
                int filesize = 1000000; // bytes
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