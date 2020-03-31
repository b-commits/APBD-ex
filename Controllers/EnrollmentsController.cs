using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost("enrollStudent")]
        public IActionResult EnrollStudent([FromBody] Student Student)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true"))
            using (SqlCommand command = new SqlCommand())
            {
                command.Parameters.AddWithValue("@indexNumber", Student.IndexNumber);
                command.Parameters.AddWithValue("@firstName", Student.FirstName);
                command.Parameters.AddWithValue("@lastName", Student.LastName);
                command.Parameters.AddWithValue("@birthDate", DateTime.Parse(Student.BirthDate.ToString()));
                command.Parameters.AddWithValue("@studies", Student.Studies);

                command.Connection = connection;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT * FROM Studies WHERE Name = @studies";

                SqlDataReader dr = command.ExecuteReader();

                if (!dr.HasRows)
                {
                    transaction.Rollback();
                    return BadRequest("Kierunek o podanej nazwie nie istnieje.");
                }

                command.CommandText = "SELECT IdStudy FROM Studies WHERE Name = @studies";
                dr.Read();
                string idStudy = dr["IdStudy"].ToString();

                command.Parameters.AddWithValue("@idStudy", idStudy);
                dr.Close();

                command.CommandText = "SELECT * FROM Enrollment WHERE IdStudy = @idStudy AND semester = 1";
                var result = command.ExecuteScalar();

                if (result == null)
                {
                    string SqlFormattedDate = (DateTime.Now).ToString("yyyy-MM-dd");
                    command.Parameters.AddWithValue("@date", SqlFormattedDate);
                    command.CommandText = "INSERT INTO Enrollment VALUES (99, 1, @idStudy, @date)";
                    command.ExecuteNonQuery();
                }

                command.CommandText = "SELECT IndexNumber FROM Student WHERE IndexNumber = @indexNumber";
                var result2 = command.ExecuteScalar();

                if (result2 == null)
                {
                    transaction.Rollback();
                    return BadRequest("Taki student już istnieje");
                }

                command.CommandText = "INSERT INTO Student VALUES (@indexNumber, @firstName, @lastName, @birthDate, 99)";
                command.ExecuteNonQuery();

                transaction.Commit();


                Enrollment enrollment = new Enrollment
                {
                    Semester = 1,
                    Studies = Student.Studies
                };

                return StatusCode((int)HttpStatusCode.Created, enrollment);

            }
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true"))
            using (SqlCommand command = new SqlCommand("promoteStudents", connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@studies", StudiesSemester.Studies);
                command.Parameters.AddWithValue("@semester", StudiesSemester.Semester);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                return Ok(200);
            }

        }







    }

}

