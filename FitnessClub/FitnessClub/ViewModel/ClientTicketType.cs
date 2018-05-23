using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.ViewModel
{
    public class ClientTicketType
    {

        public Models.Client Client { get; set; }
        public Models.TicketType TicketType{get; set;}
    }
}