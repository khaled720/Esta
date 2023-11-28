using ESTA.Controllers;
using ESTA.Helpers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ESTA.API_Controllers
{
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        public IActionResult Index(ContactEmail contactEmail)
        {
            EmailSender.Send_Mail(contactEmail.Email,contactEmail.Message,contactEmail.Subject,"Esta");
            return Ok();
        }
    }
}
