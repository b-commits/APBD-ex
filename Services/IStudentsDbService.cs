using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStudentsDbService
    {
        public Enrollment EnrollStudent([FromBody] Student Student);
        public Enrollment PromoteStudents([FromBody] StudiesSemester StudiesSemester);
        public List<Student> GetStudents();
        public Student GetStudentInfo(string indexNumber);

        public Boolean AuthorizeStudent(string user, string password);
        public Boolean SetRefreshToken(string refreshToken, string user);
        public Boolean CheckRefreshToken(string refreshToken, string user);




    }
}
