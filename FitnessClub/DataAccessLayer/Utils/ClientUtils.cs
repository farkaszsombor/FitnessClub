using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class ClientUtils
    {
        public static List<Client> getAllClients()
        {
            List<Client> clientList = new List<Client>();
            using (var ctx = new NorthwindContext())
            {
                var query = (from x in ctx.Clients select x).ToList();

                clientList.AddRange(query);
            }
            return clientList;
        }
    }
}
