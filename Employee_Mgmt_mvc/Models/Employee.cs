using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Employee_Management_MVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Invalid name, please enter correct name ")]
        public string?  Name { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only characters allowed")]
        public string?  City { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z0-9 ]*$",ErrorMessage = "No special characters allowed")]
        public string? Address { get; set; }
    }
}
