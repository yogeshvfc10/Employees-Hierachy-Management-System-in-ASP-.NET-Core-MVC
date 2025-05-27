using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class LoginPage
    {
        [Key]
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
