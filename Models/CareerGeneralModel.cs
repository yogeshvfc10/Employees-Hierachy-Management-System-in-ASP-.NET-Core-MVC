using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class CareerGeneralModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string Region { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
        public string Benefits { get; set; }
        public string Application { get; set; }
        public DateTime Deadlines { get; set; }
        public string Contacts { get; set; }
        public string Policy { get; set; }
        public string ForwardMail { get; set; }
        public bool IsDelete { get; set; }
        public string DepartmentName { get; set; }
        public string RegionName { get; set; }
        public Department department { get; set; }
        public Department departments { get; set; }
        public Region region { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentCode { get; set; }
        public string Status { get; set; }
        public string RegionID { get; set; }
        public string Busineesline { get; set; }
        public string Timezone { get; set; }
        public string pincode { get; set; }
    }
}
