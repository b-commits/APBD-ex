using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication1.DAL;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections;


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
                ArrayList students = new ArrayList();
                command.Connection = connection;
                command.CommandText = "SELECT std.FirstName, std.LastName, std.BirthDate, sts.name, enr.Semester FROM Student std, Enrollment enr, Studies sts WHERE (std.IdEnrollment = enr.IdEnrollment) AND (enr.IdStudy = sts.IdStudy)";
                connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Student st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.CourseName = dr["name"].ToString();
                    st.Semester = dr["Semester"].ToString();

                    students.Add(st);

                }
                connection.Dispose();
                string s = JsonSerializer.Serialize(students);
                return Ok(s);
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
                Student st = new Student();
               
                while (dr.Read())
                {
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.CourseName = dr["name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                }

                connection.Dispose();
                string s = JsonSerializer.Serialize(st);
                return Ok(s);
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