﻿using DataAccessLayer.Utils;
using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessClub.ViewModel
{
    public class TicketsClient
    {
        public List<Ticket> Tickets { get; set; }
        public Client Client { get; set; }
        public List<TicketType> Types { get; set; }
        public string SelectedType { get; set; }
     
        public string Date { get; set; }
        public static TicketType getTicketTypeByTypeName(string whatName)
        {
            return Mappings.MappingDtos.EntityTicketTypeToModelTicketType(TicketTypeUtils.GetTicketTypeByTypeName(whatName));
        }
    }
}