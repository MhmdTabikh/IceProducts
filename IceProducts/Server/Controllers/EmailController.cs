using IceProducts.Server.Data;
using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
using IceProducts.Shared.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

namespace IceProducts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] ContactUsInputModel contactUsInput)
        {
            try
            {
                await _emailService.SendEmail(contactUsInput);
            }
            catch
            {
                return BadRequest(new BaseResponse(false, "An error occurred while sending the email, please try again later"));
            }
            return Ok();
        }
    }
}
