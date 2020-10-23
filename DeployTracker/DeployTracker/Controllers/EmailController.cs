using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployTracker.Models;
using DeployTracker.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeployTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSenderService _emailSenderService;
        public EmailController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        [HttpPost]
        [Route(nameof(SendMessage))]
        public async Task<IActionResult> SendMessage(Email mailOptions)
        {
            await _emailSenderService.SendEmailAsync(mailOptions.ToEmail, mailOptions.Subject, mailOptions.Body);
            return Ok();
        }
    }
}
