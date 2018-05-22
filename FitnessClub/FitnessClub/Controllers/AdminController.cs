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

        //DELETE : Employee 
        public ActionResult DeleteEmployee(int Id)
        {
            return RedirectToAction("ListEmployees");
        }
        //CREATE : Employee
        public ActionResult CreateEmployee()
        {
            return View();
        }

        //CREATE : Employee Http Post
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
            return View(employee);
        }

        //GET : Tickets
        public ActionResult ListTickets()
        {
            List<DataAccessLayer.Entities.Ticket> ticketContextList = TicketUtils.GetAllTickets();
            List<Ticket> ticketList = Mappings.MappingDtos.EntityTicketLIstInToModelTicketList(ticketContextList);
            return View(ticketList);
        }

        //GET : Events
        public ActionResult ListEvents()
        {
            List<DataAccessLayer.Entities.Event> eventContextList = EventUtils.GetAllEvents();
            List<Event> eventList = Mappings.MappingDtos.EntityEventToModelEventList(eventContextList);
            return View(eventList);
        }

        //GET : Ticket Types
        public ActionResult ListTicketTypes()
        {
            List<DataAccessLayer.Entities.TicketType> typesContextList = TicketTypeUtils.GetAllTicketTypes();
            List<TicketType> typeList = Mappings.MappingDtos.EntityTicketLIstInToModelTicketTypeList(typesContextList);
            return View(typeList);
        }

        //EDIT : TicketType
        public ActionResult EditTicketTypes(TicketType Type)
        {
            return View(Type);
        }

        //Edit : TicketType Http Post
        [HttpPost]
        public ActionResult EditTicketTypes(int Id,TicketType Type)
        {
            return View(Type);
        }

        //DELETE : TicketType
        public ActionResult DeleteTicketType(int Id)
        {
            return RedirectToAction("ListTicketTypes");
        }

        //CREATE : TicketType
        public ActionResult CreateTicketType()
        {
            return View();
        }

        //CREATE : TicketType Http Post
        [HttpPost]
        public ActionResult CreateTicketType(TicketType Type)
        {
            return View(Type);
        }

        //GET : Rooms
        public ActionResult ListRooms()
        {
            List<DataAccessLayer.Entities.Room> roomContextList = RoomUtils.GetAllRooms();
            List<Room> roomList = Mappings.MappingDtos.EntityRoomToModelRoomList(roomContextList);
            return View(roomList);
        }
        
        //Edit : Rooms
        public ActionResult EditRoom(Room Room)
        {
            return View(Room);
        }

        //Edit : Rooms Http Post
        [HttpPost]
        public ActionResult EditRoom(int Id, Room Room)
        {
            return View(Room);
        }

        //Delete : Rooms
        public ActionResult DeleteRoom(int Id)
        {
            return RedirectToAction("ListRooms");
        }

        //Create : Rooms
        public ActionResult CreateRoom()
        {
            return View();
        }

        //Create : Rooms
        [HttpPost]
        public ActionResult CreateRoom(Room Room)
        {
            return View(Room);
        }
    }
}