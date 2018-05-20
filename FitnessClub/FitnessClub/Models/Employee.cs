using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public string Department { get; set; }
        public string WorkPlaceName { get; set; }
    }
}