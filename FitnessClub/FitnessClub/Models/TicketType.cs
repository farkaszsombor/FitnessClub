using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Name your ticket type !")]
        //[MinLength(length:5,ErrorMessage = "Type name is too short!")]
        public string Name { get; set; }
        public int DayNum { get; set; }
        public int OccasionNum { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}