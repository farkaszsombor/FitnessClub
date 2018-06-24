using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.ViewModel
{
    public class RoomTicket
    {
        public List<Room> Rooms { get; set; }
        public Ticket Ticket { get; set; }

    }
}