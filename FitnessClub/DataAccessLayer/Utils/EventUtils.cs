using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Transactions;

namespace DataAccessLayer.Utils
{
    public class EventUtils
    {
        public static bool InsertEvent(Client client, Ticket ticket, DateTime date, bool type, Room room, Employee inserter)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                // checking if client can acces to the gym

                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var tickType = (from c in ctx.TicketTypes
                                        where c == ticket.Type
                                        select c).FirstOrDefault();

                        if (ticket.LastLoginDate.Day == date.Day)
                        {
                            temp = false;
                        }
                        else
                        {
                            if (ticket.LoginsNum < tickType.OccasionNum && ticket.StartDate.AddDays(tickType.DayNum).Day >= date.Day)
                            {
                                temp = true;
                                ctx.Events.Add(new Event { Card = client, Ticket = ticket, Date = date, Type = type, Room = room, Inserter = inserter });
                                ctx.SaveChanges();
                            }
                            else
                            {
                                temp = false;
                            }
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        temp = false;
                    }

                }
            }
            return temp;
        }
        public static bool InsertEvent(Event e)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var inserter = (from ep in ctx.Employees
                                        where ep.Id==e.Inserter.Id
                                        select ep).ToList().FirstOrDefault();
                        e.Inserter = inserter;

                        var ticket = (from t in ctx.Tickets
                                        where t.Id == e.Ticket.Id
                                        select t).ToList().FirstOrDefault();
                        e.Ticket = ticket;

                        var room = (from r in ctx.Rooms
                                        where r.Id == e.Room.Id
                                        select r).ToList().FirstOrDefault();
                        e.Room = room;

                        var client = (from c in ctx.Clients
                                      where c.Id == e.Card.Id
                                      select c).ToList().FirstOrDefault();
                        e.Card = client;
                        
                        ctx.Events.Add(e);
                        dbContextTransaction.Commit();
                        ctx.SaveChanges();
                        temp = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        temp = false;
                    }

                }
            }
            return temp;
        }
        public static bool DeleteEvent(int eventId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Event delEvent = (from e in ctx.Events
                                          where e.Id == eventId
                                          select e).FirstOrDefault();
                        if (delEvent != null)
                        {
                            ctx.Events.Remove(delEvent);
                            ctx.SaveChanges();
                            temp = true;
                        }
                        else
                        {
                            temp = false;
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        temp = false;
                    }
                }
            }

            return temp;
        }

        public static List<Event> GetAllEvents()
        {
            List<Event> eventContextList = new List<Event>();
            using(var ctx = new NorthwindContext())
            {
                var query = from e in ctx.Events.Include(x => x.Card).Include(y => y.Ticket).Include(z => z.Room).Include(w => w.Inserter) select e;
                eventContextList.AddRange(query);
            }

            return eventContextList;
        }

    }
}
