using DocumentFormat.OpenXml.Bibliography;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EmployeeHierachy12345.Models
{
    public class CareerFilter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Department { get; set; }
        public int Region { get; set; }
        public string Description { get; set; }
        public string Qualification { get; set; }
        public string Benefits { get; set; }
        public string Application { get; set; }
        public DateOnly Deadlines { get; set; }
        public string Contacts { get; set; }
        public string Policy { get; set; }
        public string ForwardMail { get; set; }
        public bool IsDelete { get; set; }
        public List<Career> Career { get; set; }
        public List<CareerGeneralModel> CareerGeneralModel { get; set; }
        public CareerViewModel Career2 { get; set; }
        public Career Careers { get; set; }
        public Department DepartmentNames { get; set; }
        public List<Department> Departments { get; set; }
        public List<Region> Regions { get; set; }
        public Department department { get; set; }
        public Department departments { get; set; }
        public Region region { get; set; }
    }
}
