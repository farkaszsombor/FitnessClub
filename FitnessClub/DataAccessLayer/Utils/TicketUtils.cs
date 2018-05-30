using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Transactions;

namespace DataAccessLayer.Utils
{
    public class TicketUtils
    {
        //ONLY FOR TEST
        public static Client TestGetClientById()
        {
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
                var query = (from ticket in ctx.Tickets.Include(x => x.Inserter).Include(y => y.Type).Include(z => z.Card) select ticket).OrderByDescending(criteria => criteria.BuyingDate);
                ticketList.AddRange(query);
            }
            return ticketList;
        }

        //var query = from x in ctx.Clients.Include(b => b.Inserter) select x;
        public static bool InsertTicket(Client client, DateTime buyingDate, DateTime startDate, double price, Employee inserter, TicketType type, bool sure)
        {
            bool ret = false;
            if (!sure)
            {
                using (var ctx = new NorthwindContext())
                {
                    using (var dbContextTransaction = ctx.Database.BeginTransaction())
                    {
                        try
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
                                ctx.SaveChanges();
                                ret = true;
                            }
                            else
                            {   //if ticket already exists
                                ret = false;
                            }
                            dbContextTransaction.Commit();
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            ret = false;
                        }
                    }
                }
            }
            else
            {
                using (var ctx = new NorthwindContext())
                {
                    using (var dbContextTransaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            ctx.Tickets.Add(new Ticket { Card = client, BuyingDate = buyingDate, StartDate = startDate, Price = price, Inserter = inserter, Type = type });
                            ctx.SaveChanges();
                            dbContextTransaction.Commit();
                            ret = true;                         
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            ret = false;
                        }
                    }
                }
            }
            return ret;
        }

        public static bool DeleteTicketFromDatabase(Client client, DateTime buyingDate, DateTime startDate, double price, Employee inserter, TicketType type)
        {
            bool temp = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
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
                            ctx.SaveChanges();
                            temp = true;
                        }
                        else
                        {   //if ticket already exists
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

        public static bool DeleteTicketFromDatabase(int ticketId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Ticket delTicket = (from e in ctx.Tickets
                                            where e.Id == ticketId
                                            select e).FirstOrDefault();
                        if (delTicket != null)
                        {
                            ctx.Tickets.Remove(delTicket);
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

        public static bool DeleteTicket(int ticketId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Ticket delTicket = (from e in ctx.Tickets
                                            where e.Id == ticketId
                                            select e).FirstOrDefault();
                        if (delTicket != null)
                        {
                            delTicket.IsDeleted = true;
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
        
        public static List<Ticket> GetListOfTicketByClientId(int id)
        {
            List<Ticket> result = new List<Ticket>();
            using (var ctx = new NorthwindContext())
            {
                var query = (from x in ctx.Tickets.Include(x => x.Inserter).Include(y => y.Type).Include(z => z.Card)
                             where x.Card.Id == id
                            select x).ToList();

                result.AddRange(query);
            }
            return result;
        }

    }
}
