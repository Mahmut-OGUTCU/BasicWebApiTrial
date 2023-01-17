using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Abstract;
using EntityLayer.Concrete.MailModels;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("send-mail")]
        public async Task<ApiReturn<dynamic>> SendMail([FromForm] MailRequest p)
        {
            try
            {
                await mailService.SendEmailAsync(p);
                return new ApiReturn<dynamic>() { status = true, message = "Mail Gönderildi", data = p };
            }
            catch (Exception e)
            {
                return new ApiReturn<dynamic>() { status = false, message = e.Message };
            }
        }
    }
}