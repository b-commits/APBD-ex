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

        public bool AuthorizeStudent(string user, string password);
        public bool SetRefreshToken(string refreshToken, string user);
        public bool CheckRefreshToken(string refreshToken, string user);




    }
}
