using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime StartDate { get; set; }
        public double Price { get; set; }
        public string EmployeeName{ get; set; }
        public string TicketName { get; set; }
    }
}