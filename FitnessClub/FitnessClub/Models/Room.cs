﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}