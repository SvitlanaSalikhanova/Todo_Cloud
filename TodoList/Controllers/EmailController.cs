using Microsoft.AspNetCore.Mvc;
using TodoList.Services.Interfaces;

namespace TodoList.Controllers
{
    [Route("todolists/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailController> _logger;

        public EmailController( IEmailService emailService, ILogger<EmailController> logger)
        {
            _emailService = emailService;
            _logger = logger;
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
        
        [HttpGet("log")]
        public async Task<ActionResult> Log()
        {
            _logger.LogInformation("This is Information log");
            _logger.LogWarning("This i warning");
             return Ok("Email was sent");
        }
    }
}
