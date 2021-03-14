using BookMyTruck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BookMyTruck.Controllers
{

    //[RoutePrefix("Admin")]
    public class AdminController : Controller
    {
       readonly BookMyTruckDBcontext db = new BookMyTruckDBcontext();
        // GET: Admin
       // [Route("{Admindashboard}")]
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                List<Service> services = new List<Service>();
                TempData["managerRequests"] = db.Users.Where(usr => usr.UserRole == "manager" && usr.ValidUser == false && usr.UserStatus == false).Count(); ;
                return View(services);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        //[Route()]
        public ActionResult ManagerRequests()
        {
            if (Session["UserId"] != null)
            {
                List<User> managerRequests = db.Users.Where(usr => usr.UserRole == "manager" && usr.ValidUser == false && usr.UserStatus == false).ToList();
                TempData["managerRequests"] = managerRequests.Count();
                if (managerRequests.Count!=0) 
                {
                    return View(managerRequests);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
              
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SuccessManagerRequest(string id)
        {
            User user = db.Users.FirstOrDefault(usr => usr.UserId == id);
            if(user!=null)
            {
                try
                {
                    user.UserStatus = true;
                    user.ValidUser = true;
                    user.Description = "You Added as a manager";
                    db.SaveChanges();
                    return RedirectToAction("DisplayMessage","Home",new { msg = "Manager Added Successfully!!!", act = "ManagerRequests", ctrl = "Admin", isinput = false });

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }
        public ActionResult RejectManagerRequest(string id)
        {
            User user = db.Users.FirstOrDefault(usr => usr.UserId == id);
            if (user!=null)
            {
                try
                {
                    return RedirectToAction("DisplayMessage", "Home", new { msg = "Are you really want to reject??", act = "RejectManagerRequest", ctrl = "Admin", isinput = true,id=id });

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult RejectManagerRequest(Message message)
        {
            if(message.Inputdata!=null&&message.Id!=null)
            {
                User rejectUser = db.Users.FirstOrDefault(usr => usr.UserId == message.Id);
                if(rejectUser!=null)
                {
                    rejectUser.Description = message.Inputdata;
                    rejectUser.ValidUser = false;
                    rejectUser.UserStatus = true;
                    db.SaveChanges();

                    return RedirectToAction("ManagerRequests");

                }
            }
            return View();
        }

        [HttpPost]
        public  string demo()
        {
            return "Succes";
        }

    }
}