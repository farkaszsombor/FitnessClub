using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;//required for Inlcude 
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        public static bool InsertClient(string fName,string lName, string phone, string email, string identityNum,int InserterId, bool sex)
        {
            bool ret = false;
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (var ctx = new NorthwindContext())
                    {
                        ctx.Clients.Add(new Client { FirstName = fName, LastName = lName, IdentityNumber = identityNum, Phone = phone, Email = email, IsDeleted = false, Sex = false, BirthYear = 0, ImagePath = null, Inserter = null, InsertedDate = DateTime.Now });
                        ctx.SaveChanges();
                    }
                    ret = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ts.Dispose();
                    ret = false;
                }
            }
            return ret;
        }
    }
}
