using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        //private IStudentsDbService _service;
        private IStudentsDbServiceEF _service;

        //public StudentsController(IStudentsDbService service)
        //{
        //    _service = service;
        //}
        public StudentsController(IStudentsDbServiceEF service)
        {
            _service = service;
        }

        //[HttpGet("getStudents")]
        //public IActionResult GetStudents()
        //{
        //    return StatusCode(200, _service.GetStudents());
        //}
        [HttpGet("getStudents")]
        public IActionResult GetStudents()
        {
            return Ok(_service.GetStudents());
        }

        //[HttpGet("getStudentInfo/{ska}")]
        //public IActionResult GetStudentInfo(string ska)
        //{
        //    return StatusCode(200, _service.GetStudentInfo(ska));
        //}

        //[HttpPost]
        //public IActionResult CreateStudent(Student student)
        //{
        //    student.IndexNumber = $"s{new Random().Next(1, 20000)}";
        //    return Ok(student);
        //}
        //[HttpDelete]
        //public IActionResult DeleteStudent(int id)
        //{
        //    return Ok("Usuwanie zakończone");
        //}
        [HttpDelete("deleteStudent/{ska}")]
        public IActionResult DeleteStudent(string ska)
        {
            _service.DeleteStudent(ska);
            return Ok("Student " + ska+ " deleted.");
        }

        //[HttpPut]
        //public IActionResult UpdateStudent(int id)
        //{
        //    return Ok("Aktualizacja zakończona");
        //}
        [HttpPut("updateStudent")]
        public IActionResult UpdateStudent([FromBody] EntityModels.Student student)
        {
            _service.UpdateStudent(student);
            return Ok("Student " + student.IndexNumber +" updated.");
        }
 
    }
    
}