using DataAccessLayer.Utils;
using FitnessClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessClub.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginError()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee user)
        {
            if (EmployeeUtils.AuthenticationEmployee(user.Name,user.Password))
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("LoginError");
    
        }



        
    }
}
