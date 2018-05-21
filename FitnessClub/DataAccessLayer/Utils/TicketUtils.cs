using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DataAccessLayer.Utils
{
    public class TicketUtils
    {
        //ONLY FOR TEST
        public static Client TestGetClientById() {
            Client c = new Client();
            using (var ctx = new NorthwindContext())
            {

                var query = from x in ctx.Clients select x;
                c = query.FirstOrDefault();
            }
            return c;
        }

        public static Employee TestGetEmployeeById()
        {
            Employee c = new Employee();
            using (var ctx = new NorthwindContext())
            {

                var query = from x in ctx.Employees select x;
                c = query.FirstOrDefault();
            }
            return c;
        }

        public static List<Ticket> GetAllTickets()
        {
            List<Ticket> ticketList = new List<Ticket>();
            using (var ctx = new NorthwindContext())
            {
                var query = from ticket in ctx.Tickets.Include(x => x.Inserter).Include(y => y.Type).Include(z => z.Card) select ticket;
                ticketList.AddRange(query);
            }
            return ticketList;
        }

        //var query = from x in ctx.Clients.Include(b => b.Inserter) select x;
        public static bool InsertTicket(Client client, DateTime buyingDate, DateTime startDate, double price, Employee inserter, TicketType type, bool sure)
        {
            if (!sure)
            {
                using (var ctx = new NorthwindContext())
                {
                    //testing if tisket already exist
                    int resultTicket = 0;
                    var queryTicket = from t in ctx.Tickets
                                      where t.Card == client &&
                                          //t.BuyingDate == buyingDate &&
                                          t.StartDate == startDate &&
                                          t.Type == type
                                      select t.Id;
                    resultTicket = queryTicket.FirstOrDefault();
                    if (resultTicket == 0)
                    {   //if ticket doesent exists
                        ctx.Tickets.Add(new Ticket { Card = client, BuyingDate = buyingDate, StartDate = startDate, Price = price, Inserter = inserter, Type = type });
                        return true;
                    }
                    else
                    {   //if ticket already exists
                        return false;
                    }
                }
            }
            else
            {
                using (var ctx = new NorthwindContext())
                {
                    ctx.Tickets.Add(new Ticket { Card = client, BuyingDate = buyingDate, StartDate = startDate, Price = price, Inserter = inserter, Type = type });
                    return true;
                }
            }

        }

        public static bool DeleteTicketFromDatabase(Client client, DateTime buyingDate, DateTime startDate, double price, Employee inserter, TicketType type)
        {
            bool temp = false;
            using (var ctx = new NorthwindContext())
            {
                //testing if tisket already exist
                Ticket resultTicket;
                var queryTicket = from t in ctx.Tickets
                                  where t.Card == client &&
                                      //t.BuyingDate == buyingDate &&
                                      t.StartDate == startDate &&
                                      t.Type == type
                                  select t;
                resultTicket = queryTicket.FirstOrDefault();
                if (resultTicket != null)
                {   //if ticket doesent exists
                    ctx.Tickets.Remove(resultTicket);
                    temp = true;
                }
                else
                {   //if ticket already exists
                    temp = false;
                }
            }
            return temp;
        }

        public static bool DeleteTicketFromDatabase(int ticketId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                Ticket delTicket = (from e in ctx.Tickets
                                  where e.Id == ticketId
                                  select e).FirstOrDefault();
                if (delTicket != null)
                {
                    ctx.Tickets.Remove(delTicket);
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }

            return temp;
        }

        public static bool DeleteTicket(int ticketId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                Ticket delTicket = (from e in ctx.Tickets
                                    where e.Id == ticketId
                                    select e).FirstOrDefault();
                if (delTicket != null)
                {
                    delTicket.IsDeleted = true;
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }

            return temp;
        }
        
    }
}
