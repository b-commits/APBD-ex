using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EntityModels;

namespace WebApplication1.Services
{
    public interface IStudentsDbServiceEF
    {
        public List<EntityModels.Student> GetStudents();
        public void UpdateStudent(EntityModels.Student student);
        public void DeleteStudent(string ska);

    }
}
