using System.ComponentModel.DataAnnotations;
using System;


namespace EmployeeHierachy12345.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string Project { get; set; }

        [Required]
        
        public string Price { get; set; }

        [Required]
        
        public string Sales { get; set; }
    }
}
