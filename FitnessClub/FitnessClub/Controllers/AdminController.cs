using DataAccessLayer.Utils;
using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FitnessClub.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if ((Session["LoginedUser"] ?? "").ToString() == "Admin")
            {
                return View();
            }
            return RedirectToAction("LoginError", "Login");
        }

        // GET:Client
        public ActionResult ListClient()
        {
            List<DataAccessLayer.Entities.Client> layerClientList = DataAccessLayer.Utils.ClientUtils.GetAllClients();
            List<Client> clientList = Mappings.MappingDtos.EntityClientToModelClientAsList(layerClientList);
            return View(clientList);
        }

        //EDIT : Client
        public ActionResult EditClient(Client client)
        {
             return View(client);
        }
        //EDIT : Client HTTP Post
        [HttpPost]
        public ActionResult EditClient(int Id,Client client)
        {
            if (ModelState.IsValid)
            {
                bool Sex = client.Sex == "Male" ? false : true;
                ClientUtils.UpdateClient(client.Id, client.FirstName, client.LastName, client.Phone, client.Email, Sex);
                return RedirectToAction("ListClient");
            }
            return View(client);

        }
        //DELETE : Client
        public ActionResult DeleteClient(int Id)
        {
            ClientUtils.DeleteClient(Id);
            return RedirectToAction("ListClient");
        }

        //CREATE : Client
        public ActionResult CreateClient()
        {
            return View();
        }

        //CREATE : Client HTTP Post
        [HttpPost]
        public ActionResult CreateClient(Client Client)
        {
            ClientUtils.InsertClient(Mappings.MappingDtos.ModelClientToEntityClient(Client));
            return View(Client);
        }


        //GET: Employees
        public ActionResult ListEmployees()
        {
            List<DataAccessLayer.Entities.Employee> empContextList = EmployeeUtils.GetAllEmplyees();
            List<Employee> empList = new List<Employee>();
            Employee emp = new Employee();
            foreach(DataAccessLayer.Entities.Employee element in empContextList)
            {
                empList.Add(Mappings.MappingDtos.EntityEmployeeToModelEmployee(element));
            }
            return View(empList);
        }

        //EDIT: Employee
        public ActionResult EditEmployee(Employee employee)
        {
            return View(employee);
        }

        //EDIT: Employee Http Post
        [HttpPost]
        public ActionResult EditEmployee(int Id,Employee employee)
        {
            return View(employee);
        }







        public ActionResult ListTickets()
        {
            //it can be finished when there will be data in table not just null everywhere :P 
            List<DataAccessLayer.Entities.Ticket> ticketContextList = TicketUtils.GetAllTickets();
            List<Ticket> ticketList = new List<Ticket>();
            Ticket ticket = new Ticket();
            foreach(DataAccessLayer.Entities.Ticket element in ticketContextList)
            {
                ticket.Id = 1;
                ticket.BuyingDate = DateTime.Now;
                ticket.StartDate = DateTime.Now;
                ticket.Price = 12.3;
                ticket.EmployeeName = "Admin";
                ticket.TicketName = "WeightLifting";
            }
            ticket.Id = 1;
            ticket.BuyingDate = DateTime.Now;
            ticket.StartDate = DateTime.Now;
            ticket.Price = 12.3;
            ticket.EmployeeName = "Admin";
            ticket.TicketName = "WeightLifting";

            ticketList.Add(ticket);
            ticketList.Add(ticket);

            return View(ticketList);
        }

        public ActionResult ListEvents()
        {
            List<DataAccessLayer.Entities.Event> eventContextList = EventUtils.GetAllEvents();
            List<Event> eventList = new List<Event>();
            Event @event = new Event();//masked event is a keyword and we do not want use it as keyword
            foreach (DataAccessLayer.Entities.Event element in eventContextList)
            {
                @event.Id = element.Id;
                @event.Date = element.Date;
                @event.Type = element.Type;
                @event.ClientName = element.Card.FirstName + " " + element.Card.LastName;
                @event.EmployeeName = element.Inserter.Name;
                @event.RoomName = element.Room.Name;
            }
            @event.Id = 1;
            @event.Date = DateTime.Now;
            @event.Type = true;
            @event.ClientName = "Farkas Zsombor";
            @event.EmployeeName = "Admin";
            @event.RoomName = "Gyurunk wazzeg";

            eventList.Add(@event);
            eventList.Add(@event);

            return View(eventList);
        }

        public ActionResult ListTicketTypes()
        {
            List<DataAccessLayer.Entities.TicketType> typesContextList = TicketTypeUtils.GetAllTicketTypes();
            List<TicketType> typeList = new List<TicketType>();
            TicketType type = new TicketType();
            foreach(DataAccessLayer.Entities.TicketType element in typesContextList)
            {
                
            }
            type.Id = 1;
            type.DayNum = 30;
            type.OccasionNum = 1;
            type.Status = true;
            type.Price = 12.12;
            typeList.Add(type);
            typeList.Add(type);
            return View(typeList);
        }

        public ActionResult ListRooms()
        {
            List<DataAccessLayer.Entities.Room> roomContextList = RoomUtils.GetAllRooms();
            List<Room> roomList = new List<Room>();
            Room room = new Room();
            foreach(DataAccessLayer.Entities.Room element in roomContextList)
            {
                room.Id = element.Id;
                room.Name = element.Name;
                room.IsDeleted = element.IsDeleted;
                roomList.Add(room);
                roomList.Add(room);
            }

            return View(roomList);
        }
    }
}