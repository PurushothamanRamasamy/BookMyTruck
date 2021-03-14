using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMyTruck.Controllers
{
    public class CustomerController : Controller
    {
        readonly BookMyTruckDBcontext db = new BookMyTruckDBcontext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowAvaliableTrucks(string pickupCity, string dropCity, string truckType)
        {
            if (Session["UserId"]!=null)
            {
                List<Truck> searchedTrucks = db.Trucks.Where(trk => trk.PickCity == pickupCity && trk.DropCity == dropCity && trk.TruckType == truckType&&trk.BookedStatus==false&&trk.TruckStatus==true).ToList();
                return View(searchedTrucks);
            }
            else
            {
                return RedirectToAction("Index", "Admin");

            }

        }
        public ActionResult MyBookings()
        {
            if (Session["UserId"] != null)
            {
                string userId = Session["UserId"].ToString();
                List<Request> myrequests = db.Requests.Where(req => req.CustomerId == userId).ToList();

                return View(myrequests);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult BookTruck(string trucknumber,string userid)
        {
            Truck truck = db.Trucks.FirstOrDefault(trk => trk.TruckNumber == trucknumber);
            User user = db.Users.FirstOrDefault(usr => usr.UserId == userid);
            if (truck !=null && user!=null)
            {
                try
                {
                    Request request = new Request();
                    request.CustomerId = user.UserId;
                    request.ManagerId = truck.ManagerId;
                    request.PickupCity = truck.PickCity;
                    request.DropCity = truck.DropCity;
                    request.EstimatedCost = truck.CostPerKM;
                    request.TruckNumber = truck.TruckNumber;
                    request.Description = "Your Booking is under Processing";
                    request.RequestStatus = false;
                    request.AcceptStatus = false;
                    db.Requests.Add(request);
                    truck.BookedStatus = true;
                    db.SaveChanges();
                    return RedirectToAction("DisplayMessage", "Home", new { msg = "Booked Successfully!!!", act = "Index", ctrl = "Home", isinput = false });

                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View();

        }
        public ActionResult Serviced()
        {
            if (Session["UserId"] != null)
            {
                string userId = Session["UserId"].ToString();
                List<Service> myServices = db.Services.Where(req => req.CustomerId == userId &&req.ServiceStatus==true ).ToList();

                return View(myServices);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

        }
    }
}