using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Transactions;

namespace DataAccessLayer.Utils
{
    public class StatisticsUtils
    {
        public static double GetIncomeOfTheMonth(DateTime month)
        {
            double result;
            using (var ctx=new NorthwindContext())
            {
                result = (from t in ctx.Tickets.Include(x => x.Inserter).Include(y => y.Type).Include(z => z.Card)
                         where (t.BuyingDate.Year==month.Year)&&
                         (t.BuyingDate.Month==month.Month)
                         select t.Type.Price).ToList().Sum();
            }
            return result;
        }

        public static double GetIncomeOfTheYear(DateTime year)
        {
            double result;
            using (var ctx = new NorthwindContext())
            {
                result = (from t in ctx.Tickets.Include(y => y.Type)
                          where (t.BuyingDate.Year == year.Year)
                          select t.Type.Price).ToList().Sum();
            }
            return result;
        }

        public static int GetTicketsNumberOfYear(DateTime year)
        {
            int result;
            using (var ctx = new NorthwindContext())
            {
                result = (from ev in ctx.Tickets
                          where ev.BuyingDate.Year == year.Year
                          select 1).ToList().Sum();
            }
            return result;
        }

        public static int GetTicketsNumberOfMonth(DateTime month)
        {
            int result;
            using (var ctx = new NorthwindContext())
            {
                result = (from ev in ctx.Tickets
                          where ev.BuyingDate.Month == month.Month
                          select 1).ToList().Sum();
            }
            return result;
        }

        public static int GetEventsNumberOfMonth(DateTime month)
        {
            int result;
            using (var ctx = new NorthwindContext())
            {
                result = (from ev in ctx.Events
                          where ev.Date.Month == month.Month
                          select 1).ToList().Sum();
            }
            return result;
        }

        public static int GetEventsNumberOfYear(DateTime year)
        {
            int result;
            using (var ctx = new NorthwindContext())
            {
                result = (from ev in ctx.Events
                         where ev.Date.Year==year.Year
                         select 1).ToList().Sum();
            }
            return result;
        }

        public static Dictionary<Employee, int> GetAllEmployeesPerformanceEver()
        {
            Dictionary<Employee, int> statisticMap = new Dictionary<Employee, int>();
            using (var ctx = new NorthwindContext())
            {
                var employees = (from emp in ctx.Employees.Include(x=>x.WorkPlace)
                                 select emp).ToList();

                var events = (from ev in ctx.Events.Include(x=>x.Inserter).Include(x => x.Room)
                              select ev).ToList();


                foreach (var item in employees)
                {
                    statisticMap.Add(item, 0);
                }
               
                foreach (var item in events)
                {
                    string eventDay;
                    switch ((int)item.Date.DayOfWeek)
                    {
                        case 0:
                            eventDay = "V";
                            break;

                        case 1:
                            eventDay = "H";
                            break;

                        case 2:
                            eventDay = "K";
                            break;

                        case 3:
                            eventDay = "SZE";
                            break;

                        case 4:
                            eventDay = "CS";
                            break;

                        case 5:
                            eventDay = "P";
                            break;

                        default:
                            eventDay = "SZO";
                            break;
                    }
                    var trainers = (from s in statisticMap
                             where s.Key.Days.Contains(eventDay)&&
                             s.Key.WorkPlace.Id==item.Room.Id&&
                             s.Key.StartHour<=item.Date.Hour&&
                             s.Key.EndHour > item.Date.Hour
                             select s).ToList();
                    var receptionists = (from s in statisticMap
                                where s.Key.Id == item.Inserter.Id
                                select s).ToList();

                    foreach (var t in trainers.Union(receptionists))
                    {
                        statisticMap[t.Key] ++;
                    }

                }
                var ticketInserters = (from i in ctx.Tickets.Include(x => x.Inserter)
                                       select i.Inserter).ToList();

                var clientInserters = (from i in ctx.Clients.Include(x => x.Inserter)
                                       select i.Inserter).ToList();
                foreach (var t in ticketInserters)
                {
                    statisticMap[t]++;
                }
                foreach (var t in clientInserters)
                {
                    statisticMap[t]++;
                }
            }

            return statisticMap;
        }

        public static Dictionary<Employee, int> GetAllEmployeesPerformanceByMonth(DateTime month)
        {
            Dictionary<Employee, int> statisticMap = new Dictionary<Employee, int>();
            using (var ctx = new NorthwindContext())
            {
                var employees = (from emp in ctx.Employees.Include(x => x.WorkPlace)
                                 select emp).ToList();

                 var events = (from ev in ctx.Events.Include(x => x.Inserter).Include(x => x.Room)
                               select ev).ToList();


                foreach (var item in employees)
                {
                    statisticMap.Add(item, 0);
                }
               
                foreach (var item in events)
                {
                    string eventDay;
                    switch ((int) item.Date.DayOfWeek)
                    {
                        case 0:
                            eventDay = "V";
                            break;

                        case 1:
                            eventDay = "H";
                            break;

                        case 2:
                            eventDay = "K";
                            break;

                        case 3:
                            eventDay = "SZE";
                            break;

                        case 4:
                            eventDay = "CS";
                            break;

                        case 5:
                            eventDay = "P";
                            break;

                        default:
                            eventDay = "SZO";
                            break;
                    }
                    var trainers = (from s in statisticMap
                               where s.Key.Days.Contains(eventDay) &&
                               s.Key.WorkPlace.Id == item.Room.Id &&
                               s.Key.StartHour <= item.Date.Hour &&
                               s.Key.EndHour > item.Date.Hour &&
                               item.Date.Year== month.Year&&
                               item.Date.Month == month.Month
                               select s).ToList();

                    var receptionists = (from s in statisticMap
                                         where s.Key.Id == item.Inserter.Id&&
                                         item.Date.Year == month.Year &&
                                         item.Date.Month == month.Month
                                         select s).ToList();

                    foreach (var t in trainers.Union(receptionists))
                    {
                        statisticMap[t.Key]++;
                    }
                }
                var ticketInserters = (from i in ctx.Tickets.Include(x=>x.Inserter)
                               where i.BuyingDate.Year==month.Year&&
                               i.BuyingDate.Month==month.Month
                               select i.Inserter).ToList();

                var clientInserters = (from i in ctx.Clients.Include(x => x.Inserter)
                                       where i.InsertedDate.Year == month.Year &&
                                       i.InsertedDate.Year == month.Month
                                       select i.Inserter).ToList();
                foreach (var t in ticketInserters)
                {
                    statisticMap[t]++;
                }
                foreach (var t in clientInserters)
                {
                    statisticMap[t]++;
                }
            }
            return statisticMap;
        }


    }
}
