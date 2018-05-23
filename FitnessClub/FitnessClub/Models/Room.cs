using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Room name is required!")]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}