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
            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid)
                {

                    return RedirectToAction("ShowAvaliableTrucks", "Customer", new { pickupCity = getCustomer.PickUp, dropCity = getCustomer.Drop, truckType = getCustomer.Type });
                }
                else
                {
                    
                    ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
                    return View();
                }
                
            }
            else
            {

                TempData["userPickUp"] = getCustomer.PickUp;
                TempData["userDrop"] = getCustomer.Drop;
                TempData["userType"] = getCustomer.Type;
                return RedirectToAction("ValidateUser", "Home");
            }
            

            
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
                                Session["UserRole"] = user.UserRole;
                                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                                return RedirectToAction("Index", "Admin");
                            }
                            if (user.UserRole == "customer")
                            {
                                Session["UserId"] = user.UserId;
                                Session["UserRole"] = user.UserRole;
                                List<Request> userrequest = db.Requests.Where(req => req.CustomerId == user.UserId && req.RequestStatus==true).ToList();
                                TempData["myRequests"] = userrequest.Count();
                                if (TempData.Peek("userPickUp") != null)
                                {
                                    return RedirectToAction("ShowAvaliableTrucks", "Customer", new { pickupCity = TempData.Peek("userPickUp"), dropCity = TempData.Peek("userDrop"), truckType = TempData.Peek("userType") });
                                }

                                return RedirectToAction("Index", "Home");
                            }
                            if (user.UserRole == "manager")
                            {
                                Session["UserId"] = user.UserId;
                                Session["UserRole"] = user.UserRole;
                                return RedirectToAction("Index", "Manager");
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
            ViewBag.ddlRoles = dropdownlist.RoleList;
            return View(registeruser);
        }
        [HttpPost]
        public ActionResult Register(UserRegister registeredUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (registeredUser.UserRole == "Book Truck")
                    {
                        User adduser = new User();
                        adduser.Mobile = registeredUser.Mobile;
                        adduser.UserId = registeredUser.DisplayName + registeredUser.Mobile;
                        adduser.DisplayName = registeredUser.DisplayName;
                        adduser.UserRole = "customer";
                        adduser.UserStatus = true;
                        adduser.ValidUser = true;
                        adduser.Password = registeredUser.Password;
                        adduser.Description = "No Issues";
                        db.Users.Add(adduser);
                        db.SaveChanges();
                        Session["UserId"] = adduser.UserId;
                        return RedirectToAction("DisplayMessage", new { msg = "Succefully Registered", act = "Index",ctrl="Home", isinput = false });
                    }
                    if (registeredUser.UserRole == "Add My Truck")
                    {
                        User adduser = new User();
                        adduser.Mobile = registeredUser.Mobile;
                        adduser.UserId = registeredUser.DisplayName + registeredUser.Mobile;
                        adduser.DisplayName = registeredUser.DisplayName;
                        adduser.UserRole = "manager";
                        adduser.UserStatus = false;
                        adduser.ValidUser = false;
                        adduser.Password = registeredUser.Password;
                        adduser.Description = "Admin will Response Kindly wait for sometime";
                        db.Users.Add(adduser);
                        db.SaveChanges();
                        Session["UserId"] = adduser.UserId;
                        return RedirectToAction("DisplayMessage", new { msg = "Your request will be approved soon. Kindly keep check on notification we will notify you", act = "Index", ctrl = "Manager", isinput=false });
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return View();
        }

        public ActionResult DisplayMessage(string msg, string act,string ctrl,bool isinput,string id="")
        {
            Message message = new Message();
            message.DispalyMessage = msg;
            message.ToAction = act;
            message.ToControl = ctrl;
            message.IsInput = isinput;
            message.Id = id;
            message.Inputdata = "Invalid Credentials";
            return View(message);
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

        
    }
}