using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int TicketId { get; set; }
        public DateTime Date{ get; set; }
        public bool Type{ get; set; }
        public string RoomName{ get; set; }
        public string EmployeeName { get; set; }
    }
}