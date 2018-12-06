using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Repository.Models
{
    public class EmployeeEntity
    {
        public Guid? Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }        
        
        [Required]
        public string FullName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

    }
}
