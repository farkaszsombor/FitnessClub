using DataAccessLayer.Utils;
using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using FitnessClub.ViewModel;
using FitnessClub.Mappings;

namespace FitnessClub.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if ((Session["LoginedUser"] ?? "").ToString() == "Admin")
            {
                var statistics = new StatisticsModel
                {
                    EventsMonthNumbNow = StatisticsUtils.GetEventsNumberOfMonth(DateTime.Now),
                    EventsYearNumbNow = StatisticsUtils.GetEventsNumberOfYear(DateTime.Now),
                    TicketsNumberOfMonthNow = StatisticsUtils.GetTicketsNumberOfMonth(DateTime.Now),
                    TicketsNumberOfYearNow = StatisticsUtils.GetTicketsNumberOfYear(DateTime.Now),
                    IncomeOfTheMonthNow = StatisticsUtils.GetIncomeOfTheMonth(DateTime.Now),
                    IncomeOfTheYearNow = StatisticsUtils.GetIncomeOfTheYear(DateTime.Now),
                    PerformanceEverMap=MappingDtos.EntityEmployeeToModelEmployeeAsDictionary(StatisticsUtils.GetAllEmployeesPerformanceEver()),
                    PerformanceMapNow=MappingDtos.EntityEmployeeToModelEmployeeAsDictionary(StatisticsUtils.GetAllEmployeesPerformanceByMonth(DateTime.Now))
                };

                return View(statistics);
            }
            return RedirectToAction("LoginError", "Login");
        }

        // GET:Client
        public ActionResult ListClient(int? page,string searchString)
        {
            List<DataAccessLayer.Entities.Client> layerClientList = ClientUtils.GetAllClients();
            List<Client> clientList = Mappings.MappingDtos.EntityClientToModelClientAsList(layerClientList);
            if (!String.IsNullOrEmpty(searchString))
            {
                clientList = clientList.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower())
                || s.LastName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(clientList.ToPagedList(page ?? 1, pageSize : 20));
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
                if (ClientUtils.UpdateClient(Mappings.MappingDtos.ModelClientToEntityClient(client)))
                {
                    return RedirectToAction("ListClient");
                }
            }
            return View(client);

        }
        //DELETE : Client
        public ActionResult DeleteClient(int Id)
        {
            if (ClientUtils.DeleteClient(Id))
            {
                return RedirectToAction("ListClient");
            }
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
            if (ClientUtils.InsertClient(Mappings.MappingDtos.ModelClientToEntityClient(Client)))
            {
                return RedirectToAction("ListClient");
            }
            return View(Client);
        }


        //GET: Employees
        public ActionResult ListEmployees(int? page,string searchString)
        {
            List<DataAccessLayer.Entities.Employee> empContextList = EmployeeUtils.GetAllEmplyees();
            List<Employee> empList = Mappings.MappingDtos.EntityEmployeeToModelEmployeeAsList(empContextList);
            if (!String.IsNullOrEmpty(searchString))
            {
                empList = empList.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(empList.ToPagedList(page ?? 1, pageSize : 20));
        }

        //EDIT: Employee
        public ActionResult EditEmployee(Employee employee)
        {
            employee.PasswrodRep = employee.Password;
            return View(employee);
        }

        //EDIT: Employee Http Post
        [HttpPost]
        public ActionResult EditEmployee(int Id,Employee Employee)
        {
            if (ModelState.IsValid)
            {
                if (EmployeeUtils.UpdateEmployee(Mappings.MappingDtos.ModelEmployeeToEntityEmployee(Employee)))
                {
                    return RedirectToAction("ListEmployees");
                }
                return View(Employee);
            }
            return View(Employee);
        }

        //DELETE : Employee 
        public ActionResult DeleteEmployee(int Id)
        {
            if (EmployeeUtils.DeleteEmployee(Id))
            {
                return RedirectToAction("ListEmployees");
            }
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
            if (ModelState.IsValid)
            {
                if (EmployeeUtils.InsertEmployee(Mappings.MappingDtos.ModelEmployeeToEntityEmployee(employee)))
                { 
                    return RedirectToAction("ListEmployees");
                }
            }
            return View(employee);
        }

        //GET : Tickets
        public ActionResult ListTickets(int? page,string searchString,bool active = false,bool inactive = false,string category = "")
        {
            List<DataAccessLayer.Entities.Ticket> ticketContextList = TicketUtils.GetAllTickets();
            List<Ticket> ticketList = Mappings.MappingDtos.EntityTicketLIstToModelTicketAsList(ticketContextList);
            List<TicketType> type = Mappings.MappingDtos.EntityTicketLIstToModelTicketTypeAsList(TicketTypeUtils.GetAllTicketTypes());
            ViewBag.tklist = type;
            if (active && inactive && String.IsNullOrEmpty(category))
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    ticketList = ticketList.Where(s => s.TicketName.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    ticketList = ticketList.Where(s => s.TicketName.ToLower().Contains(searchString.ToLower())).ToList();
                }
                if (!String.IsNullOrEmpty(category))
                {
                    ticketList = ticketList.Where(s => s.TicketName == category).ToList();
                }
                if (active)
                {
                    List<Ticket> tic = new List<Ticket>();
                    foreach (var ticket in ticketList)
                    {
                        var ticketType = TicketTypeUtils.GetTicketTypeByTypeName(ticket.TicketName);
                        var date = ticket.StartDate;
                        if (ticketType.DayNum != 0 && ticketType.OccasionNum != 0)
                        {
                            if (ticketType.DayNum - ticket.LoginsNum > 0)
                            {
                                tic.Add(ticket);
                            }
                        }

                        if (ticketType.DayNum != 0 && date.AddDays(ticketType.DayNum) > DateTime.Now)
                        {
                            if (ticketType.DayNum - ticket.LoginsNum > 0)
                            {
                                tic.Add(ticket);
                            }
                        }
                        if (ticketType.OccasionNum != 0)
                        {
                            if (ticketType.OccasionNum - ticket.LoginsNum > 0)
                            {
                                tic.Add(ticket);
                            }
                        }
                    }

                    ticketList = tic;
                }
                if (inactive)
                {
                    List<Ticket> tic = new List<Ticket>();
                    foreach (var ticket in ticketList)
                    {
                        var ticketType = TicketTypeUtils.GetTicketTypeByTypeName(ticket.TicketName);
                        var date = ticket.StartDate;
                        if (ticketType.DayNum != 0 && ticketType.OccasionNum != 0)
                        {
                            if (ticketType.DayNum - ticket.LoginsNum < 0)
                            {
                                tic.Add(ticket);
                            }
                        }

                        if (ticketType.DayNum != 0 && date.AddDays(ticketType.DayNum) < DateTime.Now)
                        {
                            if (ticketType.DayNum - ticket.LoginsNum < 0)
                            {
                                tic.Add(ticket);
                            }
                        }
                        if (ticketType.OccasionNum != 0)
                        {
                            if (ticketType.OccasionNum - ticket.LoginsNum < 0)
                            {
                                tic.Add(ticket);
                            }
                        }
                    }

                    ticketList = tic;
                }
            }
            return View(ticketList.ToPagedList(page ?? 1,pageSize : 20));
        }

        //GET : Events
        public ActionResult ListEvents(int? page,string searchString,string category = "",string usedate ="")
        {
            int cid = 0;
            List<DataAccessLayer.Entities.Event> eventContextList = EventUtils.GetAllEvents();
            List<Event> eventList = Mappings.MappingDtos.EntityEventToModelEventList(eventContextList);
            List<TicketType> type = Mappings.MappingDtos.EntityTicketLIstToModelTicketTypeAsList(TicketTypeUtils.GetAllTicketTypes());
            ViewBag.tklist = type;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (Int32.TryParse(searchString,out cid))
                {
                    var c = ClientUtils.GetClientById(cid);
                    eventList = eventList.Where(s => s.ClientName == (c.FirstName + " " + c.LastName)).ToList();
                }
                else
                {
                    eventList = eventList.Where(s => s.ClientName.ToLower().Contains(searchString.ToLower())).ToList();
                }
            }
            if (!String.IsNullOrEmpty(category))
            {
                eventList = eventList.Where(s => s.TicketId == Int32.Parse(category)).ToList();
            }
            if (!String.IsNullOrEmpty(usedate))
            {
                eventList = eventList.Where(s => s.Date.ToString("yyyy'-'MM'-'dd").Contains(usedate)).ToList();
            }
            
            return View(eventList.ToPagedList(page ?? 1, pageSize : 20));
        }

        //GET : Ticket Types
        public ActionResult ListTicketTypes(int? page,string seachString)
        {
            List<DataAccessLayer.Entities.TicketType> typesContextList = TicketTypeUtils.GetAllTicketTypes();
            List<TicketType> typeList = Mappings.MappingDtos.EntityTicketLIstToModelTicketTypeAsList(typesContextList);
            if (!String.IsNullOrEmpty(seachString))
            {
                typeList = typeList.Where(s => s.Name.ToLower().Contains(seachString.ToLower())).ToList();
            }
            return View(typeList.ToPagedList(page ?? 1, pageSize : 20));
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
            if (ModelState.IsValid)
            {
                if (TicketTypeUtils.UpdateTicketType(Mappings.MappingDtos.ModelTicketTypeToEntityTicketType(Type)))
                {
                    return RedirectToAction("ListTicketTypes");
                }
            }
            return View(Type);
        }

        //DELETE : TicketType
        public ActionResult DeleteTicketType(int Id)
        {
            if (TicketTypeUtils.DeleteTicketType(Id))
            {
                return RedirectToAction("ListTicketTypes");
            }
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
            if (ModelState.IsValid)
            {
                if (TicketTypeUtils.InsertTicketType(Mappings.MappingDtos.ModelTicketTypeToEntityTicketType(Type)))
                {
                    return RedirectToAction("ListTicketTypes");
                }
            }
            return View(Type);
        }

        //GET : Rooms
        public ActionResult ListRooms(int? page,string searchString,string deleted)
        {
            List<DataAccessLayer.Entities.Room> roomContextList = RoomUtils.GetAllRooms();
            List<Room> roomList = Mappings.MappingDtos.EntityRoomToModelRoomAsList(roomContextList);
            if (!String.IsNullOrEmpty(searchString))
            {
                roomList = roomList.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(roomList.ToPagedList(page ?? 1,pageSize : 20));
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
            if (ModelState.IsValid)
            {
                if (RoomUtils.UpdateRoom(Mappings.MappingDtos.ModelRoomToEntityRoom(Room)))
                {
                    return RedirectToAction("ListRooms");
                }
            }
            return View(Room);
        }

        //Delete : Rooms
        public ActionResult DeleteRoom(int Id)
        {
            if (RoomUtils.DeleteRoom(Id))
            {
                return RedirectToAction("ListRooms");
            }
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
            if (ModelState.IsValid)
            {
                if (RoomUtils.InsertRoom(Room.Name))
                {
                    return RedirectToAction("ListRooms");
                }
            }
            return View(Room);
        }

        [HttpPost]
        public ActionResult SaveToXls(string what)
        {
            DataTable dt = null;
            bool result = false;
            switch (what)
            {
                case "Client":
                    List<DataAccessLayer.Entities.Client> layerClientList = ClientUtils.GetAllClients();
                    List<Client> clientList = Mappings.MappingDtos.EntityClientToModelClientAsList(layerClientList);
                    dt = DataToExcel.ConvertToDataTable(clientList);
                    result = DataToExcel.FlushToExcel<Client>(dt);
                    break;
                case "Employee":
                    List<DataAccessLayer.Entities.Employee> layerEmplyeeList = EmployeeUtils.GetAllEmplyees();
                    List<Employee> employeeList = Mappings.MappingDtos.EntityEmployeeToModelEmployeeAsList(layerEmplyeeList);
                    dt = DataToExcel.ConvertToDataTable(employeeList);
                    result = DataToExcel.FlushToExcel<Employee>(dt);
                    break;
                case "Event":
                    List<DataAccessLayer.Entities.Event> layerEventList = EventUtils.GetAllEvents();
                    List<Event> eventList = Mappings.MappingDtos.EntityEventToModelEventList(layerEventList);
                    dt = DataToExcel.ConvertToDataTable(eventList);
                    result = DataToExcel.FlushToExcel<Event>(dt);
                    break;
                case "Room":
                    List<DataAccessLayer.Entities.Room> layerRoomList = RoomUtils.GetAllRooms();
                    List<Room> roomList = Mappings.MappingDtos.EntityRoomToModelRoomAsList(layerRoomList);
                    dt = DataToExcel.ConvertToDataTable(roomList);
                    result = DataToExcel.FlushToExcel<Room>(dt);
                    break;
                case "Ticket":
                    List<DataAccessLayer.Entities.Ticket> layerTicketList = TicketUtils.GetAllTickets();
                    List<Ticket> ticketList = Mappings.MappingDtos.EntityTicketLIstToModelTicketAsList(layerTicketList);
                    dt = DataToExcel.ConvertToDataTable(ticketList);
                    result = DataToExcel.FlushToExcel<Ticket>(dt);
                    break;
                case "Type":
                    List<DataAccessLayer.Entities.TicketType> layerTypeList = TicketTypeUtils.GetAllTicketTypes();
                    List<TicketType> typeList = Mappings.MappingDtos.EntityTicketLIstToModelTicketTypeAsList(layerTypeList);
                    dt = DataToExcel.ConvertToDataTable(typeList);
                    result = DataToExcel.FlushToExcel<TicketType>(dt);
                    break;
                default:
                    break;
            }

            if (result)
            {
                return RedirectToAction("ListEmployees");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        //Email Send Actions
        public ActionResult SendEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(string header,string body)
        {
            int result = 0;
            try
            {
                GMailer.GmailUsername = "zsomborf96@gmail.com";
                GMailer.GmailPassword = "";

                GMailer mailer = new GMailer();
                List<DataAccessLayer.Entities.Client> layerClientList = ClientUtils.GetAllClients();
                List<Client> clientList = Mappings.MappingDtos.EntityClientToModelClientAsList(layerClientList);
                mailer.Subject = header;
                mailer.Body = body;
                mailer.IsHtml = true;
                foreach (var item in clientList)
                {
                    mailer.ToEmail = item.Email;
                    mailer.Send();
                }
                result = 1;
            }
            catch(Exception)
            {
                result = 0;
            }
            return RedirectToAction("SendEmailResult",new { id = result});
        }

        public ActionResult SendEmailResult()
        {
            return View();
        }

        //Sign out
        public ActionResult Logout()
        {
            Session["LoginedUser"] = "";
            return RedirectToAction("Index", "Login");
        }
    }
}