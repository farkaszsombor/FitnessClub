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


    }
}
