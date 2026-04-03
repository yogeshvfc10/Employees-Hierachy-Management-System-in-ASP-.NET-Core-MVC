namespace EmployeeHierachy12345.Models
{
    public class Filter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTime Hiredate { get; set; }
        public string Gender { get; set; }     
        public string Password { get; set; }
        public string Region { get; set; }
        public string Department { get; set; }
        public TimeOnly ShiftStartTime { get; set; }
        public TimeOnly ShiftEndTime { get; set; }
        public int Salary { get; set; }
        public bool IsActive { get; set; }
        public string Comments { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Timespan { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string RegionID { get; set; }
        public string RegionName { get; set; }

        public string Busineesline { get; set; }

        public string Timezone { get; set; }

        public string pincode { get; set; }

        public List<GeneralModel> GeneralModel { get; set; }
        public EmployeeRegister EmployeeRegister { get; set; }

        public Department DepartmentNames { get; set; }
        public List<Department> Departments { get; set; }
        public List<Region> Regions { get; set; }
        public Department department {  get; set; }

        public Department departments { get; set; }

        public Region region { get; set; }
    }
}
