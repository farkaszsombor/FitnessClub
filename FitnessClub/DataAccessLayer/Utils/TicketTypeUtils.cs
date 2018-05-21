﻿using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer.Utils
{
    public class TicketTypeUtils
    {
        public static List<TicketType> GetAllTicketTypes()
        {
            List<TicketType> result = new List<TicketType>();
            using (var ctx = new NorthwindContext())
            {
                var query = ctx.TicketTypes.Select(x => x);
                result.AddRange(query);
            }
            return result;
        }
        public static bool InsertTicketType(int dayNum, int occasionNum, bool status, double price)
        {
            bool ret = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.TicketTypes.Add(new TicketType { DayNum = dayNum, OccasionNum = occasionNum, Status = status, Price = price });
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
            return ret;           
        }
    }
}
