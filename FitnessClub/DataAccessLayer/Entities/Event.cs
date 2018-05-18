using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public Client Card { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime Date { get; set; }
        public bool Type { get; set; }
        public Room Room { get; set; }
        public Employee Inserter { get; set; }
    }
}
