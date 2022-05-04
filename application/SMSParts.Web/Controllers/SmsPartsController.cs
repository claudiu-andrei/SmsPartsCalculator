using System.Net;
using Microsoft.AspNetCore.Mvc;
using SMSParts.Business.Interfaces;
using SMSParts.Domain.Models;

namespace SMSParts.Web.Controllers
{
    [Route("SmsParts")]
    [ApiController]
    public class SmsPartsController : ControllerBase
    {
        private ISmsPartsService SmsPartsService { get; }

        public SmsPartsController(ISmsPartsService smsPartsService)
        {
            SmsPartsService = smsPartsService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string input)
        {
            var response = SmsPartsService.GetSmsPartsInformation(input);
            if (response.IsValid)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, response.Problem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetParts([FromBody] InputModelDto input)
        {
            var response = SmsPartsService.GetSmsPartsInformation(input.Text);
            if (response.IsValid)
            {
                return Ok(response.Data);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, response.Problem);
        }
    }
}
