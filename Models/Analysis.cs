using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class Analysis
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter Name")]
        public int Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please select Project")]
        public int Project { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter Task")]
        public string Task { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter WorkingHrs")]
        public int WorkingHrs { get; set; }

        [Required(ErrorMessage = "Please select Date")]
        public DateOnly Date { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Priority")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Please select Status")]
        public string Status { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Feedback")]
        public string Feedback { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please select ClientApproval")]
        public string ClientApproval { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Module")]
        public string Module { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Update")]
        public string Update { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter AuditLogs")]
        public string AuditLogs { get; set; }
        public bool IsDelete { get; set; }
    }
}
