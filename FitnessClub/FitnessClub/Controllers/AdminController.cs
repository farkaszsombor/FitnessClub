using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessClub.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<DataAccessLayer.Entities.Client> layerClientList = DataAccessLayer.Utils.ClientUtils.getAllClients();
            List<Client> clientList = new List<Client>();
            Client client = new Client();
            foreach(DataAccessLayer.Entities.Client element in layerClientList)
            {
                client.Id = element.Id;
                client.FirstName = element.FirstName;
                client.LastName = element.LastName;
                client.Phone = element.Phone;
                client.Email = element.Email;
                client.ImagePath = String.IsNullOrEmpty(element.ImagePath) ? "nincs" : element.ImagePath;
                client.isDeleted = element.IsDeleted;
                client.InsertedDate = element.InsertedDate;
                client.IdentityNumber = element.IdentityNumber;
                client.Sex = element.Sex;
                client.InserterName = element.Inserter.Name;
                clientList.Add(client);
                clientList.Add(client);
            }
            return View(clientList);
        }
    }
}