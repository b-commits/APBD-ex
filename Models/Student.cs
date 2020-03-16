namespace WebApplication1
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }

        public Student(int IdStudent, string FirstName, string LastName)
        {
            this.IdStudent = IdStudent;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public Student() { }

    }
}
