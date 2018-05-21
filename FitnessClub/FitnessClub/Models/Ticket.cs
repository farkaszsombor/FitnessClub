using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int LoginsNum { get; set; }
        public double Price { get; set; }
        public string EmployeeName{ get; set; }
        public string TicketName { get; set; }
        public bool IsDeleted { get; set; }
        public int RemaningLoginNum { get; set; }
        public DateTime EndDate { get; set; }
    }
}