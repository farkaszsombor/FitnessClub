﻿using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;

namespace DataAccessLayer.Utils
{
    public class ClientUtils
    {
        public static List<Client> GetAllClients()
        {
            List<Client> clientList = new List<Client>();
            using (var ctx = new NorthwindContext())
            {
                var query = from x in ctx.Clients.Include(b => b.Inserter) select x;
                clientList.AddRange(query);
            }
            return clientList;
        }

        public static bool InsertClient(Client client)
        {
            bool result = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var q = (from y in ctx.Employees where y.Name == client.Inserter.Name select y).FirstOrDefault();
                        client.Inserter = q;
                        ctx.Clients.Add(client);
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool UpdateClient(Client Client)
        {
            bool result = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Client query = (from x in ctx.Clients where x.Id == Client.Id select x).FirstOrDefault();
                        ctx.Entry(query).CurrentValues.SetValues(Client);
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool DeleteClient(int Id)
        {
            bool result = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Client query = (from client in ctx.Clients where client.Id == Id select client).First();
                        if (!query.IsDeleted)
                        {
                            query.IsDeleted = true;
                            ctx.SaveChanges();
                            result = true;
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
                return result;
            }
        }

        public static Client GetClientByParameters(Client user)
        {
            Client result;
            using (var ctx = new NorthwindContext())
            {
                var query = (from x in ctx.Clients.Include(b => b.Inserter)
                            where x.FirstName==user.FirstName &&
                                  x.LastName==user.LastName &&
                                  x.Phone ==user.Phone &&
                                  x.IdentityNumber== user.IdentityNumber
                             select x).FirstOrDefault();

                result = query;
            }
            return result;
        }

        public static Client GetClientById (int Id)
        {
            Client result;

            using(var ctx = new NorthwindContext())
            {
                result = (from client in ctx.Clients.Include(x => x.Inserter) where client.Id == Id select client).ToList().FirstOrDefault();
            }
            if (result != null)
            {
                return result;
            }
            else
            {
                return new Client();
            }

        }

        public static Client GetClientByTicketId(int ticketId)
        {
            Client result;

            using (var ctx = new NorthwindContext())
            {
                var tmp = (from t in ctx.Tickets.Include(x=>x.Card)
                           where t.Id == ticketId
                           select t).ToList().FirstOrDefault(); ;
                result = tmp.Card;
            }
            return result;
        }
    }
}
