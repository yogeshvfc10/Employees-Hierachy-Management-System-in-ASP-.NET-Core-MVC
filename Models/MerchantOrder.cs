using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeHierachy12345.Models
{
    public class MerchantOrder
    {
        [Key]
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string RazorpayKey { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        
    }
}







