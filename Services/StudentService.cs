using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentService : IStudentsDbService
    {
        private const string ConnectionString = "Data Source=db-mssql;Initial Catalog=s19677;Integrated Security=true";
        public Enrollment EnrollStudent([FromBody] Student Student)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
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
                }

                command.CommandText = "SELECT IdStudy FROM Studies WHERE Name = @studies";
                dr.Read();
                string idStudy = dr["IdStudy"].ToString();
                dr.Close();

                command.Parameters.AddWithValue("@idStudy", idStudy);

                command.CommandText = "SELECT IdEnrollment FROM Enrollment WHERE IdStudy = @idStudy AND semester = 1";
                string newEnrol = command.ExecuteScalar().ToString();

                command.CommandText = "SELECT * FROM Enrollment WHERE IdStudy = @idStudy AND semester = 1";
                var result = command.ExecuteScalar();

                if (result == null)
                {
                    string SqlFormattedDate = (DateTime.Now).ToString("yyyy-MM-dd");
                    command.Parameters.AddWithValue("@date", SqlFormattedDate);
                    command.CommandText = "INSERT INTO Enrollment VALUES (99, 1, @idStudy, @date)";
                    newEnrol = "99";
                    command.ExecuteNonQuery();
                }

                command.Parameters.AddWithValue("@newEnrol", newEnrol);

                command.CommandText = "SELECT IndexNumber FROM Student WHERE IndexNumber = @indexNumber";
                var result2 = command.ExecuteScalar();

                if (result2 != null)
                {
                    transaction.Rollback();
                }

                command.CommandText = "INSERT INTO Student VALUES (@indexNumber, @firstName, @lastName, @birthDate, @newEnrol, null, null, null)";
                command.ExecuteNonQuery();

                transaction.Commit();


                Enrollment enrollment = new Enrollment
                {
                    Semester = 1,
                    Studies = Student.Studies
                };

                return enrollment;

            }
        }

        public Enrollment PromoteStudents([FromBody] StudiesSemester StudiesSemester)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("promoteStudents", connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@studies", StudiesSemester.Studies);
                command.Parameters.AddWithValue("@semester", StudiesSemester.Semester);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                Enrollment enrollment = new Enrollment
                {
                    Semester = StudiesSemester.Semester + 1,
                    Studies = StudiesSemester.Studies
                };

                return enrollment;
            }
        }

        public Student GetStudentInfo(string ska)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT std.IndexNumber, std.FirstName, std.LastName, std.BirthDate, sts.name, enr.Semester FROM Student std, Enrollment enr, Studies sts WHERE (std.IdEnrollment = enr.IdEnrollment) AND (enr.IdStudy = sts.IdStudy) AND std.IndexNumber ='" + ska + "';";
                SqlDataReader dr = command.ExecuteReader();
                Student student = new Student();

                while (dr.Read())
                {
                    student.IndexNumber = dr["IndexNumber"].ToString();
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = (DateTime)dr["BirthDate"];
                }

                return student;
            }
        }

        public List<Student> GetStudents()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand())

            {
                List<Student> students = new List<Student>();
                command.Connection = connection;
                command.CommandText = "SELECT std.IndexNumber, std.FirstName, std.LastName, std.BirthDate, sts.name, enr.Semester FROM Student std, Enrollment enr, Studies sts WHERE (std.IdEnrollment = enr.IdEnrollment) AND (enr.IdStudy = sts.IdStudy)";
                connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Student student = new Student();

                    student.IndexNumber = dr["IndexNumber"].ToString();
                    student.FirstName = dr["FirstName"].ToString();
                    student.LastName = dr["LastName"].ToString();
                    student.BirthDate = (DateTime)dr["BirthDate"];

                    students.Add(student);

                }
                return students;
            }
        }

        public static bool CheckIndex(string indexNumber)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.Parameters.AddWithValue("@index", indexNumber);

                command.CommandText = "SELECT 1 FROM Student WHERE indexNumber = @index";
                var result = command.ExecuteScalar();

                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public bool AuthorizeStudent(string user, string password)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("user", user);
                    command.Parameters.AddWithValue("password", password);
                    command.CommandText = "SELECT 1 FROM Student WHERE IndexNumber = @user and Password = @password;";
                    return command.ExecuteReader().Read();
                }
                catch (SqlException ex)
                {
                    return false;
                }

            }
        }

        public bool SetRefreshToken(string refreshToken, string user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("refreshToken", refreshToken);
                    command.Parameters.AddWithValue("user", user);
                    command.CommandText = "UPDATE Student SET RefreshToken = @refreshToken WHERE IndexNumber = @user";
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }

        public bool CheckRefreshToken(string refreshToken, string user)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                connection.Open();
                try
                {
                    command.Parameters.AddWithValue("refreshToken", refreshToken);
                    command.Parameters.AddWithValue("user", user);
                    Console.WriteLine(refreshToken);
                    command.CommandText = "SELECT 1 FROM Student WHERE IndexNumber = @user AND refreshToken = @refreshToken";
                    return command.ExecuteReader().Read();
                }
                catch (SqlException ex)
                {
                    return false;
                }
            }
        }
    }
}
