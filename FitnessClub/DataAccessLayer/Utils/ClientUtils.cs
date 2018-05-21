using DataAccessLayer.Contexts;
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

        public static void InsertClient(Client client)
        {
            using(var ctx = new NorthwindContext())
            {
                var q = (from y in ctx.Employees where y.Name == client.Inserter.Name select y).FirstOrDefault();
                client.Inserter = q;
                ctx.Clients.Add(client);
                ctx.SaveChanges();
            }
        }


        public static void InsertClient(string fName,string lName, string phone, string email, string identityNum,int InserterId, bool sex)
        {
            using (var ctx = new NorthwindContext())
            {
                ctx.Clients.Add(new Client { FirstName = fName, LastName = lName,IdentityNumber=identityNum,Phone=phone,Email=email,IsDeleted=false,Sex=false,BirthYear=0,ImagePath=null,Inserter=null,InsertedDate=DateTime.Now });
                ctx.SaveChanges();
            }
        }

        public static void UpdateClient(int Id,string Fname,string Lname,string Phone,string Email,bool Sex)
        {
            using(var ctx = new NorthwindContext())
            {
                Client query = (from x in ctx.Clients where x.Id == Id select x).First();
                query.FirstName = Fname;
                query.LastName = Lname;
                query.Phone = Phone;
                query.Email = Email;
                query.Sex = Sex;
                ctx.SaveChanges();
            }
        }

        public static bool DeleteClient(int Id)
        {
            using(var ctx = new NorthwindContext())
            {
                Client query = (from client in ctx.Clients where client.Id == Id select client).First();
                if (!query.IsDeleted)
                {
                    query.IsDeleted = true;
                    ctx.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
    }
}
