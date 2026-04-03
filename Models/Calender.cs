using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class Calender
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
