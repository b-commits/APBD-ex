using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.Services;

namespace WebApplication1
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public StudentsController(IStudentsDbService service)
        {
            _service = service;
        }

        [HttpGet("getStudents")]
        public IActionResult GetStudents()
        {
            return StatusCode(200, _service.GetStudents());
        }

        [HttpGet("getStudentInfo/{ska}")]
        public IActionResult GetStudentInfo(string ska)
        {
            return StatusCode(200, _service.GetStudentInfo(ska));
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie zakończone");
        }
        [HttpPut]
        public IActionResult UpdateStudent(int id)
        {
            return Ok("Aktualizacja zakończona");
        }



    }
}