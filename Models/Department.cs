namespace EmployeeHierachy12345.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentID  { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set;}
        public bool IsDelete { get; set; }
    }
}
