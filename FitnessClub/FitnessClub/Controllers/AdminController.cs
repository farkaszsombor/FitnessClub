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
            List<DataAccessLayer.Entities.Client> alma = DataAccessLayer.Utils.ClientUtils.getAllClients();
            ViewBag.Message = alma.Count;
            Client client = new Client();
            foreach(DataAccessLayer.Entities.Client element in alma)
            {
                client.LastName = element.LastName;
            }
            return View();
        }
    }
}