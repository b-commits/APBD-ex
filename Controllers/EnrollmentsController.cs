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
            _service.EnrollStudent(Student);


            Enrollment enrollment = new Enrollment
            {
                Semester = 1,
                Studies = Student.Studies
            };

            return StatusCode((int)HttpStatusCode.Created, enrollment);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        {
            _service.PromoteStudents(StudiesSemester);

            Enrollment enrollment = new Enrollment
            {
                Semester = StudiesSemester.Semester + 1,
                Studies = StudiesSemester.Studies
            };

            return StatusCode((int)HttpStatusCode.Created, enrollment);
        }

    }







}



