using System;

namespace DataAccessLayer.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Client Card { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime StartDate { get; set; }
        public double Price { get; set; }
        public Employee Inserter { get; set; }
        public TicketType Type { get; set; }
    }
}