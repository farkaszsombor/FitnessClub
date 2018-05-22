﻿
using DataAccessLayer.Utils;
using FitnessClub.Models;
using FitnessClub.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessClub.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            if ((Session["LoginedUser"] ?? "").ToString() != "")
            {
                return View();
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
            var data = MappingDtos.EntityTicketLIstInToModelTicketTypeList(TicketTypeUtils.GetAllTicketTypes());
            return View(data);
        }
        public ActionResult TicketsList(Client client)
        {
            var data=MappingDtos.EntityTicketLIstInToModelTicketList(TicketUtils.GetListOfTicketByClientId(client.Id));
            return View(data);

        }

        public ActionResult TicketTypeItem(TicketType tic)
        {
            return View(tic);
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Insert(Client user)
        {
            ClientUtils.InsertClient(user.FirstName, user.LastName, user.Phone, user.Email, user.IdentityNumber, 1, true);
            return RedirectToAction("Index");
        }
    }
}