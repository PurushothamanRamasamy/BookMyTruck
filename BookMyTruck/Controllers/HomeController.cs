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
        readonly BookMyTruckDBcontext db = new BookMyTruckDBcontext();
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
                /*have to comment*/
                ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
                ViewBag.Rmessage = getCustomer.PickUp + ", " + getCustomer.Drop + ", " + getCustomer.Type;
                return View();
            }

            ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
            return View();
        }
        public ActionResult ValidateUser()
        {
            return View();
        }
        public  ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login login)
        
        {
            if (ModelState.IsValid)
            {
                if (login != null)
                {
                    try
                    {
                        User user = db.Users.FirstOrDefault(usr => usr.Mobile == login.Mobile&&usr.Password==login.Password);
                        if (user != null)
                        {
                            if (user.UserRole == "Admin")
                            {
                                Session["UserId"] = user.UserId;
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                return RedirectToAction("Admindashboard", "Admin");
                            }
                        }
                        else
                        {
                            TempData["userError"] = "Incorrect Credentials please try again";
                            return RedirectToAction("ValidateUser");

                        }
                    }
                    catch (Exception ex)
                    {
                        return View(ex);
                    }
                }
            }
            return View();
        }

        public ActionResult Register(string mobile)
        {
            UserRegister registeruser = new UserRegister();
            registeruser.Mobile = mobile;
            return View(registeruser);
        }
        [HttpPost]
        public ActionResult Register(UserRegister registereduser)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    User adduser = new User();
                    adduser.Mobile = registereduser.Mobile;
                    adduser.UserId = registereduser.DisplayName +registereduser.Mobile;
                    adduser.DisplayName = registereduser.DisplayName;
                    adduser.UserRole = registereduser.UserRole;
                    adduser.UserStatus = false;
                    adduser.Password = registereduser.Password;
                    

                    db.Users.Add(adduser);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return View();
        }
        public bool IsUserAlreadyExists(string Mobile)
        {

            var user = (from usr in db.Users 
                               where usr.Mobile==Mobile.ToString() 
                               select usr).ToList();
            if (user.Count != 0)
                return true;
            else return false;
        }

        public JsonResult IsUser(string Mobile)
        {
            var user = (from usr in db.Users
                        where usr.Mobile == Mobile.ToString()
                        select usr.Password);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            
           
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult ChooseRegister()
        {
            return View();
        }

    }
}