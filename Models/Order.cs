using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeHierachy12345.Models
{
    public class Order
    {
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Please enter Name.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Gmail.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Gmail is not valid.")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        [Required(ErrorMessage = "Please enter PhoneNo.")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar(5)")]
        [Required(ErrorMessage = "Please enter Amount.")]
        public string Amount { get; set; }

    }
}
