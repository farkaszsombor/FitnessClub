using DataAccessLayer.Utils;
using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

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
        public ActionResult ListTickets(int? page,string searchString)
        {
            List<DataAccessLayer.Entities.Ticket> ticketContextList = TicketUtils.GetAllTickets();
            List<Ticket> ticketList = Mappings.MappingDtos.EntityTicketLIstInToModelTicketAsList(ticketContextList);
            if (!String.IsNullOrEmpty(searchString))
            {
                ticketList = ticketList.Where(s => s.TicketName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(ticketList.ToPagedList(page ?? 1,pageSize : 20));
        }

        //GET : Events
        public ActionResult ListEvents(int? page,string searchString)
        {
            List<DataAccessLayer.Entities.Event> eventContextList = EventUtils.GetAllEvents();
            List<Event> eventList = Mappings.MappingDtos.EntityEventToModelEventList(eventContextList);
            if (!String.IsNullOrEmpty(searchString))
            {
                eventList = eventList.Where(s => s.ClientName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(eventList.ToPagedList(page ?? 1, pageSize : 20));
        }

        //GET : Ticket Types
        public ActionResult ListTicketTypes(int? page,string seachString)
        {
            List<DataAccessLayer.Entities.TicketType> typesContextList = TicketTypeUtils.GetAllTicketTypes();
            List<TicketType> typeList = Mappings.MappingDtos.EntityTicketLIstInToModelTicketTypeAsList(typesContextList);
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
        public ActionResult ListRooms(int? page,string searchString)
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
            switch (what)
            {
                case "Client":
                    List<DataAccessLayer.Entities.Client> layerClientList = ClientUtils.GetAllClients();
                    List<Client> clientList = Mappings.MappingDtos.EntityClientToModelClientAsList(layerClientList);
                    dt = DataToExcel.ConvertToDataTable(clientList);
                    break;
                default:
                    break;
            }
            
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DataTable.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }

    }
}