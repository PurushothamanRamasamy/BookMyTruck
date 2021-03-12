using BookMyTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMyTruck.Controllers
{
    public class HomeController : Controller
    {
        BookMyTruckDBcontext db = new BookMyTruckDBcontext();
        readonly DropDown dropdownlist = new DropDown();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
            return View();
        }
        [HttpPost]
        public ActionResult Index(GetCustomerPlaces getCustomer)
        {
            
            if (ModelState.IsValid)
            {
                ModelState.Clear();
/*have to comment*/ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
                ViewBag.Rmessage=getCustomer.PickUp+", "+getCustomer.Drop+", "+getCustomer.Type;
                return View();
            }

            ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
            return View();
        }
        public  ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if(login!=null)
            {
                try
                {
                    User user = db.Users.FirstOrDefault(usr => usr.Mobile == login.Mobile && usr.Password == login.Password);
                    if(user!=null)
                    {
                        if(user.UserRole=="Admin")
                        {
                            return RedirectToAction("Admindashboard","Admin");
                        }
                    }
                }
                catch(Exception ex)
                {
                    return View(ex);
                }
            }
            return View();
        }

    }
}