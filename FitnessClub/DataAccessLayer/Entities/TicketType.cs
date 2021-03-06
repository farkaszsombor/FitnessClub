﻿namespace DataAccessLayer.Entities
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayNum { get; set; }
        public int OccasionNum { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}