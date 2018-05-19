using System;

namespace DataAccessLayer.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Client Card { get; set; }
        public DateTime BuyingDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int LoginsNum { get; set; }
        public double Price { get; set; }
        public Employee Inserter { get; set; }
        public TicketType Type { get; set; }
        public bool IsDeleted { get; set; }
    }
}