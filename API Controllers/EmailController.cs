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
        public IActionResult Index()
        {
            EmailSender.Send_Mail("info@estaegypt.org", "test test","test","Esta");
            return Ok();
        }
    }
}
