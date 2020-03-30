using System;
using System.Text.Json.Serialization;

namespace WebApplication1
{
    [Serializable]
    public class Student
    {
        public int IdStudent { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }
        [JsonPropertyName("IndexNumber")]
        public string IndexNumber { get; set; }
        [JsonPropertyName("BirthDate")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("CourseName")]
        public string CourseName { get; set; }
        [JsonPropertyName("Semester")]
        public string Semester { get; set; }


        public Student(int IdStudent, string FirstName, string LastName)
        {
            this.IdStudent = IdStudent;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public Student() { }

    }
}
