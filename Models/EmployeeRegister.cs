using System.ComponentModel.DataAnnotations;
using System;

namespace EmployeeHierachy12345.Models
{
    public class EmployeeRegister
    {
        [ScaffoldColumn(false)]
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email ID")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter text area box.")]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter state")]
        public string State { get; set; }

        public DateOnly Hiredate { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please select Region")]
        public int Region { get; set; }

        [Required(ErrorMessage = "Please select Department")]
        public int Department { get; set; }
        
        [Display(Name = "Shift Start Time")]
        [Required(ErrorMessage = "Please enter Shift Start-time")]
        public TimeOnly ShiftStartTime { get; set; }

        [Display(Name = "Shift End Time")]
        [Required(ErrorMessage = "Please enter Shift End-time")]
        public TimeOnly ShiftEndTime { get; set; }

        [Display(Name = "Salary")]
        [Required(ErrorMessage = "Please enter Salary")]
        public int Salary { get; set; }

        [Display(Name = "Is Active")]
        [Required(ErrorMessage = "Please enter IsActive status")]
        public bool IsActive { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter Comments..")]
        public string Comments { get; set; }

        public bool IsDelete { get; set; }

    }
}