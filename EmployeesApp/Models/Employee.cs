using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeesApp.Models
{
    
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="{0} is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public int Age { get; set; }
        [Display(Name="Account Number")]
        [Required(ErrorMessage = "{0} is required.")]
        public string AccountNumber { get; set; }
    }
}
