
using DataAccessLayer.Utils;
using FitnessClub.Models;
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
            return View();
        }
        // GET: Employee/TicketTypeLists
        public ActionResult TicketTypesList()
        {
            var tickType = TicketTypeUtils.GetAllTicketTypes();
            List <TicketType> list = new List<TicketType>();
            TicketType temp = new TicketType();
            foreach (var item in tickType)
            {
                temp.Id = item.Id;
                temp.OccasionNum = item.OccasionNum;
                temp.Status = item.Status;
                temp.Price = item.Price;
                temp.DayNum = item.DayNum;
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
                list.Add(temp);
            }
            return View(list);
        }
    }
}