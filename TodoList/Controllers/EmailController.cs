using Microsoft.AspNetCore.Mvc;
using TodoList.Services.Interfaces;

namespace TodoList.Controllers
{
    [Route("todolists/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController( IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("")]
        public async Task<ActionResult> SendEmail()
        {
            var sendEmailResult = await _emailService.SendEmail();
            if (sendEmailResult)
                return Ok("Email was sent");
            else
                return BadRequest("Something went wrong with email");
        }
    }
}
