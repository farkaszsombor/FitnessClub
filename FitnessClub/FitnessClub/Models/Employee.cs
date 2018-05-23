using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Client name must not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(length: 8,ErrorMessage = "Password is too sort")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Reenter the password")]
        [MinLength(length: 8, ErrorMessage = "Password is too sort")]
        [Compare("Password",ErrorMessage = "Password do not match")]
        public string PasswrodRep { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Your department is required")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Your work place name required")]
        public string WorkPlaceName { get; set; }
    }
}