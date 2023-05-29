using ESTA.Controllers;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESTA.API_Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork appRep;
        private readonly IWebHostEnvironment hostEnvironment;

        public CoursesController(IUnitOfWork appRep, IWebHostEnvironment hostEnvironment)
        {
            this.appRep = appRep;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            var courses = await appRep.CoursesRep.GetAllCourses();
            return courses;
        }

        //[HttpPost]
        //public bool SendEmail(EmailClass email)
        //{
        //    return EmailSender.Send_Mail(
        //        "k900000001@gmail.com",
        //        email.Message,
        //        email.Subject,
        //        "Contact"
        //    );
        //}
    }
}
