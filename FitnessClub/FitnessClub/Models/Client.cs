using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public bool isDeleted { get; set; }
        public DateTime InsertedDate { get; set; }
        public string IdentityNumber { get; set; }
        public string Sex { get; set; }
        public string InserterName { get; set; }
    }
}