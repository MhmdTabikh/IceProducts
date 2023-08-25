using IceProducts.Server.Services.interfaces;
using IceProducts.Shared.InputModels;
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
        public async Task<IActionResult> Send(ContactUsInputModel contactUsInput)
        {
            await _emailService.SendEmail(contactUsInput);
            return Ok();
        }
    }
}
