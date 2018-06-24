using DataAccessLayer.Contexts;
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
            bool result = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.TicketTypes.Add(new TicketType { DayNum = dayNum, OccasionNum = occasionNum, Status = status, Price = price/*, StartHour=StartHour, EndHour=endHour */});
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

        public static bool InsertTicketType(TicketType type)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.TicketTypes.Add(type);
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

        public static bool UpdateTicketType(TicketType Type)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        TicketType Query = (from t in ctx.TicketTypes where Type.Id == t.Id select t).FirstOrDefault();
                        ctx.Entry(Query).CurrentValues.SetValues(Type);
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

        public static TicketType GetTicketById(int ticketType)
        {
            TicketType result = new TicketType();
            using (var ctx = new NorthwindContext())
            {
                result = (from t in ctx.TicketTypes where t.Id == ticketType select t).FirstOrDefault();

            }
            return result;
        }

        public static TicketType GetTicketTypeByTypeName(string whatName)
        {
            TicketType result = new TicketType();
            using (var ctx = new NorthwindContext())
            {
                result = (from t in ctx.TicketTypes where t.Name == whatName select t).FirstOrDefault();

            }
            return result;
        }
    }
}
