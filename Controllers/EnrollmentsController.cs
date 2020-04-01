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
        private IStudentsDbService _service;

        public EnrollmentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpPost("enrollStudent")]
        public IActionResult EnrollStudent([FromBody] Student Student)
        {
            return StatusCode((int)HttpStatusCode.Created, _service.EnrollStudent(Student));
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        {
            return StatusCode((int)HttpStatusCode.Created, _service.PromoteStudents(StudiesSemester));
        }

    }







}



