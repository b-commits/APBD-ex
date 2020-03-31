using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Student
    {
        [Required]
        public string IndexNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Studies { get; set; }


        public Student() { }

    }
}
