using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.ViewModel
{
    public class StatisticsModel
    {
        public int EventsMonthNumbNow { get; set; }
        public int EventsYearNumbNow { get; set; }
        public double IncomeOfTheMonthNow { get; set; }
        public double IncomeOfTheYearNow { get; set; }
        public double TicketsNumberOfMonthNow { get; set; }
        public double TicketsNumberOfYearNow { get; set; }
        public Dictionary<Employee, int> PerformanceEverMap { get; set; }
        public Dictionary<Employee, int> PerformanceMapNow { get; set; }
    }
}