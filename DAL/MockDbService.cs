using System.Collections.Generic;

namespace WebApplication1.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        static MockDbService()
        {
            _students = new List<Student>
            {
                new Student{IdStudent = 5, FirstName = "Jan", LastName = "Janowski"},
                new Student{IdStudent = 2, FirstName = "Andrzej", LastName = "Andrzejewski"},
                new Student{IdStudent = 3, FirstName = "Piotr", LastName = "Piotrowski"}
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }

    }
}
