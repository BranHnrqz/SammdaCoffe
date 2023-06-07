using SammdaCoffe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SammdaCoffe.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(string user, string paswo)
        {

            using (SammdasCoffeeEntities bd = new SammdasCoffeeEntities())
            {
                var list = bd.UserType.FirstOrDefault(a => a.userMail == user && a.userPassword == paswo);
                if (list != null)
                {

                    return RedirectToAction("Menu", "Home");


                }
                else
                {
                    return View();
                }
            }

        }
    }
}