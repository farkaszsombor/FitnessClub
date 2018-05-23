using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class TicketsClient
    {
        public List<Ticket> Tickets { get; set; }
        public Client Client { get; set; }
    }
}