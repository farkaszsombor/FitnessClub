using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Utils
{
    public class CreateDatabase
    {
        public static void InitDatabase()
        {
            using (var ctx = new NorthwindContext())
            {
                var boolean = false;
                Room r = new Room { Name = "Work", IsDeleted = boolean };
                Employee e = new Employee { Name = "Admin", Password = "1234", IsDeleted = boolean, Department = "Admin", WorkPlace = r };
                ctx.Rooms.Add(r);
                ctx.Employees.Add(e);
                ctx.Clients.Add(new Client { FirstName = "Timea", LastName = "Fulop", Phone = "742975896", Email = "fuloptimea1427@gmail.com", IsDeleted = boolean, BirthYear = 1997, InsertedDate = DateTime.Now, IdentityNumber="2970821191291", Sex = true, Inserter = e });

                ctx.SaveChanges();
            }
        }
    }
}
