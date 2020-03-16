using System.Collections.Generic;

namespace WebApplication1.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
