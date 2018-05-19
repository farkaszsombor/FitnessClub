using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class EventUtils
    {
        /*public static bool InsertEvent(Client client, Ticket ticket, DateTime date, bool type, Room room, Employee inserter)
        {
            using (var ctx = new NorthwindContext()) {
                // checking if client can acces to the gym
                int result = 0;
                //kliens bemehet a terembe, ha a berleten van meg alkalom vagy ha aznap meg nem volt ott
                var query = from t in ctx.Tickets
                            where
                            select t.Id;
                result = query.FirstOrDefault();

                if (result == 0) { 
                    ctx.Events.Add(new Event { Card = client, Ticket = ticket, Date = date, Type = type, Room = room, Inserter = inserter });
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }*/

    }
}
