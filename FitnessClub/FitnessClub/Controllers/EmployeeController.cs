﻿using DataAccessLayer.Utils;
using FitnessClub.Models;
using FitnessClub.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessClub.ViewModel;
using System.IO;

namespace FitnessClub.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            if ((Session["LoginedUser"] ?? "").ToString() != "")
            {
                return RedirectToAction("ClientsList", "Employee");
            }
            return RedirectToAction("LoginError","Login");

        }

        // GET: Employee
        public ActionResult ClientsList()
        {
            var layerClientList = DataAccessLayer.Utils.ClientUtils.GetAllClients();
            List<Client> clientList = new List<Client>();
            Client client;
            foreach (var element in layerClientList)
            {
                client = new Client();
                client.Id = element.Id;
                client.FirstName = element.FirstName;
                client.LastName = element.LastName;
                client.Phone = element.Phone;
                client.Email = element.Email;
                client.ImagePath = String.IsNullOrEmpty(element.ImagePath) ? "nincs" : element.ImagePath;
                client.IsDeleted = element.IsDeleted;
                client.InsertedDate = element.InsertedDate;
                client.IdentityNumber = element.IdentityNumber;
                client.Sex = element.Sex ? "Male" : "Female";
                clientList.Add(client);
            }
            clientList = clientList.OrderBy(x=>x.LastName).ToList();
            return View(clientList);
       
        }

        // GET: Employee/TicketTypeLists
        public ActionResult TicketTypesList()
        {
            var data = MappingDtos.EntityTicketLIstToModelTicketTypeAsList(TicketTypeUtils.GetAllTicketTypes());
            return View(data);
        }
        public ActionResult TicketsList(Client client)
        {
            if (Session["TicType"] != null)
            {
                List<TicketType> SelectType = new List<TicketType>();
                SelectType.Add(MappingDtos.EntityTicketTypeToModelTicketType(TicketTypeUtils.GetTicketById(Int32.Parse(Session["TicType"].ToString()))));
                Session["TicType"] = null;
                var data = MappingDtos.EntityTicketLIstToModelTicketAsList(TicketUtils.GetListOfTicketByClientId(client.Id));
                return View(new TicketsClient { Client = client, Tickets = data, Types = SelectType });
            }
            else
            {
                var data = MappingDtos.EntityTicketLIstToModelTicketAsList(TicketUtils.GetListOfTicketByClientId(client.Id));
                return View(new TicketsClient { Client = client, Tickets = data, Types = MappingDtos.EntityTicketLIstToModelTicketTypeAsList(TicketTypeUtils.GetAllTicketTypes()) });
            }
        }

        public ActionResult TicketTypeItem(TicketType tic)
        {
            ClientTicketType mix = new ClientTicketType { TicketType = tic };
            return View(mix);
        }

        public ActionResult BuyingTicket(FormCollection collection)
        {
            int Id = Int32.Parse(collection.Get("Id"));
            int Type = Int32.Parse(collection.Get("TicketTypeId"));
            if (MappingDtos.EntityClientToModelClient(ClientUtils.GetClientById(Id)) != null)
            {
                Session["TicType"]=Type.ToString();
                return RedirectToAction("TicketsList", "Employee", MappingDtos.EntityClientToModelClient(ClientUtils.GetClientById(Id)));
            }
            else
            {
                return RedirectToAction("EmployeeError", "Employee", new { @errorMsg = "Nincs ilyen Id-vel rendelkezo kliens!" });
            }
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Insert(Client user, HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {

                // code for saving the image file to a physical location.
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                file.SaveAs(path);
                user.ImagePath = "~/Content/Images/"+ fileName;
            }
                user.InserterName = Session["LoginedUser"].ToString();
            
            if(ClientUtils.InsertClient(MappingDtos.ModelClientToEntityClient(user)))
            {
                return RedirectToAction("TicketsList", "Employee",ClientUtils.GetClientByParameters(MappingDtos.ModelClientToEntityClient(user)));
            }
            else
            {
                return RedirectToAction("EmployeeError","Employee", new { @errorMsg="Nem sikeres beszuras" });
            }
        }
        public ActionResult EmployeeError(string errorMsg)
        {

            ViewBag.MyString = errorMsg;
            return View();
        }
        public ActionResult AddTicket(FormCollection collection)
        {
            var ClientId = Int32.Parse(collection.Get("ClientId"));
            var InsertedDate = DateTime.Parse(collection.Get("InsertedDate"));
            var SelectedTypeId = Int32.Parse(collection.Get("SelectedType"));
            var StartDate = DateTime.Parse(collection.Get("StartDate"));

            if (TicketUtils.InsertTicket(ClientId, Session["LoginedUser"].ToString(), SelectedTypeId, InsertedDate, StartDate))
            {
                return RedirectToAction("TicketsList", "Employee", MappingDtos.EntityClientToModelClient(ClientUtils.GetClientById(ClientId)));
            }
            else
            {
                return RedirectToAction("EmployeeError", "Employee", new { @errorMsg = "Nem sikeres beszuras" });
            }
        }
        public ActionResult Enter(Ticket ticket)
        {
            return View(new RoomTicket { Rooms = MappingDtos.EntityRoomToModelRoomAsList(RoomUtils.GetAllRooms()),Ticket=ticket});
        }
        public ActionResult Login(FormCollection collection)
        {
            var room= collection.Get("room");
            var type = Int32.Parse(collection.Get("type"));
            var ticketId = Int32.Parse(collection.Get("ticketId"));
            var ticketOvner = collection.Get("ticketOvner");
            var modelEvent = new Event { RoomName = room, EmployeeName = Session["LoginedUser"].ToString(), ClientName = ticketOvner, Date = DateTime.Now, TicketId = ticketId, Type = Convert.ToBoolean(type) };
            var tmp = MappingDtos.ModelEventToEntityEvent(modelEvent);
            if (EventUtils.InsertEvent(tmp))
            {
                TicketUtils.EventTriggerUpdate(ticketId);
       //         List<Ticket> tickets = new List<Ticket>();
                Ticket tick = new Ticket();
                tick = MappingDtos.EntityTicketToModelTicket(TicketUtils.GetTicketById(ticketId));
      /*          tickets.Add(tick);
                Client client = new Client();
                client = MappingDtos.EntityClientToModelClient(tmp.Card);
    */            
                return RedirectToAction("SuccessEnter", "Employee", tick);
            }
            else
            {
                return RedirectToAction("EmployeeError", "Employee", new { @errorMsg = "Nem sikeres belepes" });
            }
        }
        public ActionResult SuccessEnter(Ticket param )
        {
           
            return View(param);
        }

        public ActionResult Logout()
        {
            Session["LoginedUser"] = "";
            return RedirectToAction("Index", "Login");
        }
    }
}