using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.DAL;
using System.Data.SqlClient;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;

namespace WebApplication1
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet("getStudents")]
        public IActionResult GetStudents(string orderBy)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true"))
            using (SqlCommand command = new SqlCommand())

            {
                List<Student> students = new List<Student>();
                command.Connection = connection;
                command.CommandText = "SELECT std.FirstName, std.LastName, std.BirthDate, sts.name, enr.Semester FROM Student std, Enrollment enr, Studies sts WHERE (std.IdEnrollment = enr.IdEnrollment) AND (enr.IdStudy = sts.IdStudy)";
                connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Student student = new Student();
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = (DateTime)dr["BirthDate"];
                    student.CourseName = dr["name"].ToString();
                    student.Semester = dr["Semester"].ToString();

                    students.Add(student);

                }
                connection.Dispose();
                return Ok(students);
            }
           
        }

        // Wydzielić powtórzenia.
        [HttpGet("getStudentInfo/{id}")]
        public IActionResult GetStudentInfo(int id)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true"))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT std.FirstName, std.LastName, std.BirthDate, sts.name, enr.Semester FROM Student std, Enrollment enr, Studies sts WHERE (std.IdEnrollment = enr.IdEnrollment) AND (enr.IdStudy = sts.IdStudy) AND std.IndexNumber ="+id+";";
                SqlDataReader dr = command.ExecuteReader();
                Student student = new Student();
               
                while (dr.Read())
                {
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = (DateTime)dr["BirthDate"];
                    student.CourseName = dr["name"].ToString();
                    student.Semester = dr["Semester"].ToString();
                }

                return Ok(student);
            }
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