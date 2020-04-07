using Microsoft.AspNetCore.Mvc;
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
    }
}
