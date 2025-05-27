using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class Career
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public int Department { get; set; }

        [Required]
        public int Region { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string Benefits { get; set; }

        [Required]
        public string Application { get; set; }

        [Required]
        public DateOnly Deadlines { get; set; }

        [Required]
        public string Contacts { get; set; }

        [Required]
        public string Policy { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string ForwardMail { get; set; }
        public bool IsDelete { get; set; }
    }
}
