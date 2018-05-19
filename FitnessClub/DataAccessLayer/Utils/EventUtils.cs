using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
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
                    }
                    else
                    {
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
                Event delEvent= (from e in ctx.Events
                                where e.Id == eventId
                                select e).FirstOrDefault();
                if (delEvent != null)
                {
                    ctx.Events.Remove(delEvent);
                    temp = true;
                }
                else
                {
                    temp = false;
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
