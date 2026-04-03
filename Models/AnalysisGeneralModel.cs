using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class AnalysisGeneralModel
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public int WorkingHrs { get; set; }
        public DateTime Date { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Feedback { get; set; }
        public string ClientApproval { get; set; }
        public string Module { get; set; }
        public string Update { get; set; }
        public string AuditLogs { get; set; }
        public bool IsDelete { get; set; }

        public EmployeeRegister Names { get; set; }

        public Sale Projects { get; set; }
    }
}
