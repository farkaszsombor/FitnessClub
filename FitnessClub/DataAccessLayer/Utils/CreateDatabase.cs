using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Linq;
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
                Room b = new Room { Name = "Blue", IsDeleted = boolean };
                Room re = new Room { Name = "Red", IsDeleted = boolean };
                Room p = new Room { Name = "Pink", IsDeleted = boolean };
                Room black = new Room { Name = "Black", IsDeleted = boolean };
                Room w = new Room { Name = "White", IsDeleted = boolean };
                Room g = new Room { Name = "Green", IsDeleted = boolean };
                Room y = new Room { Name = "Yellow", IsDeleted = boolean };


                Employee e1 = new Employee { Name = "Admin", Password = "1234", IsDeleted = boolean, Department = "Admin", WorkPlace = r };
                Employee e2 = new Employee { Name = "herr54y", Password = "4411", IsDeleted = boolean, Department = "Trainer", WorkPlace = b };
                Employee e3 = new Employee { Name = "james42", Password = "12354", IsDeleted = boolean, Department = "Guest", WorkPlace = re };
                Employee e4 = new Employee { Name = "katy96", Password = "12734", IsDeleted = boolean, Department = "Worker", WorkPlace = p };
                Employee e5 = new Employee { Name = "adamkis", Password = "17234", IsDeleted = boolean, Department = "IT", WorkPlace = black };
                Employee e6 = new Employee { Name = "mark87", Password = "12347", IsDeleted = boolean, Department = "cleaner", WorkPlace = w };
                Employee e7 = new Employee { Name = "peter45", Password = "158234", IsDeleted = boolean, Department = "Boss", WorkPlace = g };
                Employee e8 = new Employee { Name = "stean56", Password = "12834", IsDeleted = boolean, Department = "Trainer", WorkPlace = y };
                Employee e9 = new Employee { Name = "loise37", Password = "18234", IsDeleted = boolean, Department = "IT", WorkPlace = b };
                Employee e10 = new Employee { Name = "corina21", Password = "182234", IsDeleted = boolean, Department = "Admin", WorkPlace = re };
                Employee e11 = new Employee { Name = "helen57", Password = "182334", IsDeleted = boolean, Department = "Worker", WorkPlace = y };
                Employee e12 = new Employee { Name = "annie68", Password = "1824534", IsDeleted = boolean, Department = "Student", WorkPlace = g };
                Employee e13 = new Employee { Name = "brian24", Password = "182834", IsDeleted = boolean, Department = "Student", WorkPlace = w };
                Employee e14 = new Employee { Name = "david34", Password = "186234", IsDeleted = boolean, Department = "Boss", WorkPlace = black };
                Employee e15 = new Employee { Name = "stephen_hawking", Password = "186234", IsDeleted = boolean, Department = "IT", WorkPlace = b };


                ctx.Rooms.Add(r);
                ctx.Rooms.Add(b);
                ctx.Rooms.Add(re);
                ctx.Rooms.Add(p);
                ctx.Rooms.Add(black);
                ctx.Rooms.Add(w);
                ctx.Rooms.Add(g);
                ctx.Rooms.Add(y);

                ctx.Employees.Add(e1);
                ctx.Employees.Add(e2);
                ctx.Employees.Add(e3);
                ctx.Employees.Add(e4);
                ctx.Employees.Add(e5);
                ctx.Employees.Add(e6);
                ctx.Employees.Add(e7);
                ctx.Employees.Add(e8);
                ctx.Employees.Add(e9);
                ctx.Employees.Add(e10);
                ctx.Employees.Add(e11);
                ctx.Employees.Add(e12);
                ctx.Employees.Add(e13);
                ctx.Employees.Add(e14);
                ctx.Employees.Add(e15);

                Client c1 = new Client { FirstName = "Timea", LastName = "Fulop", Phone = "742975896", Email = "fuloptimea1427@gmail.com", IsDeleted = boolean, BirthYear = 1997, InsertedDate = DateTime.Now, IdentityNumber = "2970821191291", Sex = true, Inserter = e1 };
                Client c2 = new Client { FirstName = "Adam", LastName = "Gal", Phone = "742973336", Email = "fadam@gmail.com", IsDeleted = boolean, BirthYear = 1992, InsertedDate = DateTime.Now, IdentityNumber = "2970821191292", Sex = false, Inserter = e2 };
                Client c3 = new Client { FirstName = "Geza", LastName = "Peter", Phone = "742975896", Email = "peter@gmail.com", IsDeleted = boolean, BirthYear = 1987, InsertedDate = DateTime.Now, IdentityNumber = "2970821191293", Sex = false, Inserter = e3 };
                Client c4 = new Client { FirstName = "Peter", LastName = "Kovacs", Phone = "711975896", Email = "kovacs@gmail.com", IsDeleted = boolean, BirthYear = 1940, InsertedDate = DateTime.Now, IdentityNumber = "2970821191294", Sex = false, Inserter = e4 };
                Client c5 = new Client { FirstName = "Imre", LastName = "Kelemen", Phone = "744975896", Email = "kelemen@gmail.com", IsDeleted = boolean, BirthYear = 1970, InsertedDate = DateTime.Now, IdentityNumber = "2970821191295", Sex = false, Inserter = e5 };
                Client c6 = new Client { FirstName = "Kati", LastName = "Szasz", Phone = "742775896", Email = "kati@gmail.com", IsDeleted = boolean, BirthYear = 1989, InsertedDate = DateTime.Now, IdentityNumber = "2970821191296", Sex = true, Inserter = e6 };
                Client c7 = new Client { FirstName = "Otto", LastName = "Szabo", Phone = "742973396", Email = "otto@gmail.com", IsDeleted = boolean, BirthYear = 1998, InsertedDate = DateTime.Now, IdentityNumber = "2970821191297", Sex = false, Inserter = e7 };
                Client c8 = new Client { FirstName = "David", LastName = "GAl", Phone = "742975006", Email = "david@gmail.com", IsDeleted = boolean, BirthYear = 1999, InsertedDate = DateTime.Now, IdentityNumber = "2970821191298", Sex = true, Inserter = e8 };
                Client c9 = new Client { FirstName = "Medea", LastName = "Kovacs", Phone = "742445896", Email = "medea@gmail.com", IsDeleted = boolean, BirthYear = 1996, InsertedDate = DateTime.Now, IdentityNumber = "2970821191299", Sex = true, Inserter = e9 };
                Client c10 = new Client { FirstName = "Mihaly", LastName = "Kelemen", Phone = "733975896", Email = "mihely@gmail.com", IsDeleted = boolean, BirthYear = 1985, InsertedDate = DateTime.Now, IdentityNumber = "29708211912910", Sex = true, Inserter = e1 };
                Client c11 = new Client { FirstName = "Levente", LastName = "Szasz", Phone = "741175896", Email = "levente@gmail.com", IsDeleted = boolean, BirthYear = 1991, InsertedDate = DateTime.Now, IdentityNumber = "2970821191281", Sex = false, Inserter = e1 };
                Client c12 = new Client { FirstName = "Alice", LastName = "Orban", Phone = "742444896", Email = "alice@gmail.com", IsDeleted = boolean, BirthYear = 1970, InsertedDate = DateTime.Now, IdentityNumber = "2970821191271", Sex = true, Inserter = e1 };
                ctx.Clients.Add(c1);
                ctx.Clients.Add(c2);
                ctx.Clients.Add(c3);
                ctx.Clients.Add(c4);
                ctx.Clients.Add(c5);
                ctx.Clients.Add(c6);
                ctx.Clients.Add(c7);
                ctx.Clients.Add(c8);
                ctx.Clients.Add(c9);
                ctx.Clients.Add(c10);
                ctx.Clients.Add(c11);
                ctx.Clients.Add(c12);

                TicketType tt1 = new TicketType { Id = 1, Name = "honapos", DayNum = 30, /*OccasionNum= ,*/ Status = true, Price = 80, Description = "honapos berlet", StartHour = 14, EndHour = 16};
                TicketType tt2 = new TicketType { Id = 2, Name = "30alkalmas",/*DayNum= 30,*/ OccasionNum = 30, Status = true, Price = 90, Description = "30 alkalmas berlet", StartHour = 0, EndHour = 24 };
                TicketType tt3 = new TicketType { Id = 3, Name = "10alkalmas",/*DayNum= 30, */OccasionNum = 10, Status = true, Price = 30, Description = "10 alkalmas berlet", StartHour = 12, EndHour = 14 };
                TicketType tt4 = new TicketType { Id = 4, Name = "honapos+30alkalmas", DayNum = 30, OccasionNum = 30, Status = true, Price = 100, Description = "honapos berlet 30 alkalommal", StartHour = 12, EndHour = 14 };

                ctx.TicketTypes.Add(tt1);
                ctx.TicketTypes.Add(tt2);
                ctx.TicketTypes.Add(tt3);
                ctx.TicketTypes.Add(tt4);

                Ticket t1 = new Ticket { Id = 1, Card = c1, BuyingDate = DateTime.Now, StartDate = DateTime.Now, LastLoginDate = DateTime.Now, LoginsNum = 0, Price = 50, Inserter = e1, IsDeleted = false, Type = tt1};
                Ticket t2 = new Ticket { Id = 2, Card = c9, BuyingDate = DateTime.Now, StartDate = DateTime.Now, LastLoginDate = DateTime.Now, LoginsNum = 0, Price = 40, Inserter = e3, IsDeleted = false, Type = tt2};
                Ticket t3 = new Ticket { Id = 3, Card = c2, BuyingDate = DateTime.Now, StartDate = DateTime.Now, LastLoginDate = DateTime.Now, LoginsNum = 0, Price = 70, Inserter = e7, IsDeleted = false, Type = tt3};
                Ticket t4 = new Ticket { Id = 4, Card = c7, BuyingDate = DateTime.Now, StartDate = DateTime.Now, LastLoginDate = DateTime.Now, LoginsNum = 0, Price = 90, Inserter = e9, IsDeleted = false, Type = tt1};

                ctx.Tickets.Add(t1);
                ctx.Tickets.Add(t2);
                ctx.Tickets.Add(t3);
                ctx.Tickets.Add(t4);


                Event ee1 = new Event { Id = 1, Card = c1, Ticket = t1, Date = DateTime.Now, Type = true, Room = r, Inserter = e1 };
                Event ee2 = new Event { Id = 2, Card = c2, Ticket = t2, Date = DateTime.Now, Type = true, Room = black, Inserter = e1 };
                Event ee3 = new Event { Id = 3, Card = c3, Ticket = t3, Date = DateTime.Now, Type = true, Room = y, Inserter = e2 };
                Event ee4 = new Event { Id = 4, Card = c4, Ticket = t1, Date = DateTime.Now, Type = true, Room = w, Inserter = e3 };
                Event ee5 = new Event { Id = 5, Card = c5, Ticket = t2, Date = DateTime.Now, Type = true, Room = re, Inserter = e4 };
                Event ee6 = new Event { Id = 6, Card = c6, Ticket = t3, Date = DateTime.Now, Type = true, Room = p, Inserter = e2 };
                Event ee7 = new Event { Id = 7, Card = c9, Ticket = t1, Date = DateTime.Now, Type = true, Room = g, Inserter = e1 };
                Event ee8 = new Event { Id = 8, Card = c8, Ticket = t3, Date = DateTime.Now, Type = true, Room = r, Inserter = e2 };

                ctx.Events.Add(ee1);
                ctx.Events.Add(ee2);
                ctx.Events.Add(ee3);
                ctx.Events.Add(ee4);
                ctx.Events.Add(ee5);
                ctx.Events.Add(ee6);
                ctx.Events.Add(ee7);
                ctx.Events.Add(ee8);

                ctx.SaveChanges();
            }
        }
    }
}
