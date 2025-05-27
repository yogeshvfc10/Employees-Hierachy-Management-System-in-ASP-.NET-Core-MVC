namespace EmployeeHierachy12345.Models
{
    public class AnalysisFilter
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Project { get; set; }
        public string Task { get; set; }
        public int WorkingHrs { get; set; }
        public DateOnly Date { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Feedback { get; set; }
        public string ClientApproval { get; set; }
        public string Module { get; set; }
        public string Update { get; set; }
        public string AuditLogs { get; set; }
        public bool IsDelete { get; set; }
        public List<AnalysisGeneralModel> AnalysisGeneralModel { get; set; }
        public Analysis Analysis { get; set; }
        public EmployeeRegister Names { get; set; }
        public List<Sale> Sale { get; set; }
        public List<EmployeeRegister> Employee { get; set; }
        public Sale Projects { get; set; }
    }
}
