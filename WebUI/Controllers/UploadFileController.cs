using Exam2C2P.Application.Transactions.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
                using (var r = new StreamReader(file.OpenReadStream()))
                {

                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        //table.Add(Regex.Split(line, @"\s|[;]|[,]"));
                    }

                    r.Close();
                }
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load(file.OpenReadStream());
                //XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("Transactions");
                //string proID = "", proName = "", price = "";
                //foreach (XmlNode node in nodeList)
                //{
                //    proID = node.SelectSingleNode("Transaction id").InnerText;
                //    //proName = node.SelectSingleNode("Product_name").InnerText;
                //    //price = node.SelectSingleNode("Product_price").InnerText;
                //    //MessageBox.Show(proID + " " + proName + " " + price);
                //}
                var id = 0;
               //  var id = await Mediator.Send(FileUploadCommand command);
                return Ok(id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}