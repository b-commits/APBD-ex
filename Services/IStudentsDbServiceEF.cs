using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EntityModels;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IStudentsDbServiceEF
    {
        public List<EntityModels.Student> GetStudents();
        public void UpdateStudent(EntityModels.Student student);
        public void DeleteStudent(string ska);

        public void EnrollStudent(Student student);
        public void PromoteStudents(StudiesSemester studiesSemester);


    }
}
