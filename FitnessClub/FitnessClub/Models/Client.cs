using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required field")]
        public string LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^07[0-9]{8}$", ErrorMessage = "Not romanian phone number")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Email is not in valid form!")]
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Range(1950,2010,ErrorMessage = "Birth year must be in 1950 and 2010")]
        public int BirthYear { get; set; }
        public DateTime InsertedDate { get; set; }
        [RegularExpression(@"^[0-9]{13}$",ErrorMessage = "Invalid romanian identity number")]
        public string IdentityNumber { get; set; }
        [Required]
        public string Sex { get; set; }
        public string InserterName { get; set; }
    }
}