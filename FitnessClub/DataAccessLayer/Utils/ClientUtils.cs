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

        public static bool UpdateClient(int Id, string Fname, string Lname, string Phone, string Email, bool Sex)
        {
            bool result = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Client query = (from x in ctx.Clients where x.Id == Id select x).FirstOrDefault();
                        query.FirstName = Fname;
                        query.LastName = Lname;
                        query.Phone = Phone;
                        query.Email = Email;
                        query.Sex = Sex;
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
    }
}
