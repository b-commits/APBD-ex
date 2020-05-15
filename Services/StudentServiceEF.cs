using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EntityModels;

namespace WebApplication1.Services
{
    class StudentServiceEF : IStudentsDbServiceEF
    {
        public void DeleteStudent(string ska)
        {
            s19677Context db = new s19677Context();
            EntityModels.Student student = db.Student.SingleOrDefault(s => s.IndexNumber == ska);
            db.Student.Remove(student);
            db.SaveChanges();
        }

        public List<EntityModels.Student> GetStudents()
        {
            s19677Context db = new s19677Context();
            var res = db.Student.ToList();
            return res;
        }

        public void UpdateStudent(EntityModels.Student student)
        {
            s19677Context db = new s19677Context();
            var res = new EntityModels.Student
            {
                IndexNumber = student.IndexNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate
            };
            db.Attach(res); // res w systemie śledzenia zmian.
            db.Entry(res).Property("FirstName").IsModified = true;
            db.Entry(res).Property("LastName").IsModified = true;
            db.Entry(res).Property("BirthDate").IsModified = true;

            db.SaveChanges();
        }

    }
}
