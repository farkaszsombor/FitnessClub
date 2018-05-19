using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class TicketTypeUtils
    {
        public static List<TicketType> GetAllTicketTypes()
        {
            List<TicketType> result = new List<TicketType>();
            using (var ctx = new NorthwindContext())
            {
                var query = ctx.TicketTypes.Select(x=>x).ToList();
                result.AddRange(query);
            }
            return result;
        }
        public static void InsertTicketType(int dayNum, int occasionNum, bool status, double price)
        {
            using (var ctx = new NorthwindContext())
            {
                ctx.TicketTypes.Add(new TicketType { DayNum=dayNum,OccasionNum=occasionNum,Status=status,Price=price});
                ctx.SaveChanges();
            }
        }
    }
}
