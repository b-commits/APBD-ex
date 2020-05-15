using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        //private IStudentsDbService _service;
        private IStudentsDbServiceEF _service;


        //public EnrollmentsController(IStudentsDbService service)
        //{
        //    _service = service;
        //}
        public EnrollmentsController(IStudentsDbServiceEF service)
        {
            _service = service;
        }


        //[HttpPost("enrollStudent")]
        //[Authorize(Roles = "employee")]
        //public IActionResult EnrollStudent([FromBody] Student Student)
        //{
        //    return StatusCode((int)HttpStatusCode.Created, _service.EnrollStudent(Student));
        //}
        [HttpPost("enrollStudent")]
        public IActionResult EnrollStudent([FromBody] Student Student)
        {
            _service.EnrollStudent(Student);
            return Ok("Student enrolled");
        }



        //[HttpPost("promotions")]
        //[Authorize(Roles = "employee")]
        //public IActionResult PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        //{
        //    return StatusCode((int)HttpStatusCode.Created, _service.PromoteStudents(StudiesSemester));
        //}
        [HttpPost("promotions")]
        public IActionResult PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        {
            _service.PromoteStudents(StudiesSemester);
            return Ok("Student enrolled");
        }

    }

}



