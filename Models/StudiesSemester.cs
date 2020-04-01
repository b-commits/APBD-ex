using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class StudiesSemester
    {
        [Required]
        public string Studies { get; set; }
        [Required]
        public int Semester { get; set; }

    }
}
