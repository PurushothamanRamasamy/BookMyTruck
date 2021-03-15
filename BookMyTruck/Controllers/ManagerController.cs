using BookMyTruck.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMyTruck.Controllers
{
    public class ManagerController : Controller
    {
        readonly BookMyTruckDBcontext db = new BookMyTruckDBcontext();
        readonly DropDown dropdownlist = new DropDown();
        // GET: Manager
        public ActionResult Index()
        {
            if (Session["UserId"]!=null && Session["UserRole"].ToString()== "manager")
            {
                string MngrId = Session["UserId"].ToString();
                Session["IsValidManager"] = db.Users.FirstOrDefault(usr => usr.UserId==MngrId && usr.UserStatus == true && usr.ValidUser==true);
                List<Truck> trucks = db.Trucks.Where(truck=>truck.ManagerId== MngrId).ToList();
                List<Request> bookingrRequests = db.Requests.Where(req => req.ManagerId==MngrId && req.RequestStatus==false&&req.AcceptStatus==false).ToList();
                TempData["bookingrRequests"] = bookingrRequests.Count();
                return View(trucks);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult AddTruck()
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                Truck truck = new Truck();
                truck.ManagerId = Session["UserId"].ToString();
                ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
                return View(truck);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult AddTruck(Truck addtruck)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        addtruck.TruckStatus = false;
                        addtruck.BookedStatus = false;
                        addtruck.ManagerId = Convert.ToString(Session["UserId"]);
                        db.Trucks.Add(addtruck);
                       
                        db.SaveChanges();
                        return RedirectToAction("DisplayMessage", "Home", new { msg = "Your truck added suuccessfully,kindly enable your truck to provide Service", act = "Index", ctrl = "Manager", isinput = false, });
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting  
                                // the current instance as InnerException  
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }

                }
                ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult UpdateTruck(string id)
        {
            ViewBag.ddlTruckType = dropdownlist.TruckTypeList;
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                string mngrId = Session["UserId"].ToString();
                Truck truck = db.Trucks.FirstOrDefault(trk => trk.ManagerId == mngrId && trk.TruckNumber == id);

                return View(truck);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
        [HttpPost]
        public  ActionResult UpdateTruck(Truck truck)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(truck).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("DisplayMessage", "Home", new { msg = "Your truck has been suuccessfully updated", act = "Index", ctrl = "Manager", isinput = false, });

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
            return View();
        }
        public ActionResult DeleteTruck(string id)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                try
                {
                    Request isTruckinRequest = db.Requests.FirstOrDefault(req => req.TruckNumber == id);
                    if (isTruckinRequest==null)
                    {
                        Truck truck = db.Trucks.FirstOrDefault(trk => trk.TruckNumber == id);
                        db.Trucks.Remove(truck);
                        db.SaveChanges();
                        return RedirectToAction("DisplayMessage", "Home", new { msg = " Your truck has successfully deleted!", act = "Index", ctrl = "Manager", isinput = false, });

                    }
                    else
                    {
                        return RedirectToAction("DisplayMessage", "Home", new { msg = "This truck has booking Request, so you can't delete this truck", act = "Index", ctrl = "Manager", isinput = false, });
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult EnableTruck(string truckNumber)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                try
                {
                    Truck truck = db.Trucks.FirstOrDefault(trk=>trk.TruckNumber==truckNumber);
                    truck.TruckStatus = true;
                    db.SaveChanges();
                    return RedirectToAction("DisplayMessage", "Home", new { msg = "Your truck has been suuccessfully Enabled for Bookings", act = "Index", ctrl = "Manager", isinput = false, });

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
        public ActionResult DisableTruck(string truckNumber)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                try
                {
                    Truck truck = db.Trucks.FirstOrDefault(trk => trk.TruckNumber == truckNumber);
                    truck.TruckStatus = false;
                    db.SaveChanges();
                    return RedirectToAction("DisplayMessage", "Home", new { msg = "Disabled Successfully. Your truck will not shown to users for Bookings", act = "Index", ctrl = "Manager", isinput = false, });

                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult BookingRequest()
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                string mngrId = Session["UserId"].ToString();
                List<Request> requests = db.Requests.Where(req => req.ManagerId == mngrId && req.RequestStatus==false && req.AcceptStatus==false).ToList();
                if (requests.Count()!=0)
                {
                    return View(requests);
                }
                else
                {
                    return RedirectToAction("Index", "Manager");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public ActionResult AcceptBookingRequest(string requestId)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                int reqid = Convert.ToInt32(requestId);
                Request request = db.Requests.FirstOrDefault(req => req.RequestId == reqid);
                Service service = new Service();
                Truck truck = db.Trucks.FirstOrDefault(trk => trk.TruckNumber == request.TruckNumber);
                if (request != null)
                {
                    try
                    {
                        truck.BookedStatus = true;

                        request.RequestStatus = true;
                        request.AcceptStatus = false;
                        request.Description = "Successfully booked";
                        
                        service.CustomerId = request.CustomerId;
                        service.ManagerId = request.ManagerId;
                        service.PickupCity = request.PickupCity;
                        service.DropCity = request.DropCity;
                        service.ServiceEndDate = System.DateTime.Now;
                        service.ServiceStatus = false;
                        service.SericeCost = request.EstimatedCost;
                        service.ratings = 0;
                        service.TruckNumber = request.TruckNumber;
                        service.Description = "Service Started";



                        db.Services.Add(service);
                        db.SaveChanges();
                        return RedirectToAction("DisplayMessage", "Home", new { msg = "Booked Successfully!!!", act = "BookingRequest", ctrl = "Manager", isinput = false });

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Manager");
            }
        }
        public ActionResult RejectBookingRequest(string requestId)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                int reqid = Convert.ToInt32(requestId);
                Request request = db.Requests.FirstOrDefault(req => req.RequestId == reqid);
                if (request != null)
                {
                    try
                    {
                        return RedirectToAction("DisplayMessage", "Home", new { msg = "Are you really want to reject??", act = "RejectBookingRequest", ctrl = "Manager", isinput = true, id = requestId });

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Manager");
            }
        }
        [HttpPost]
        public ActionResult RejectBookingRequest(Message message)
        {
            if (message.Inputdata != null && message.Id != null)
            {
                int msgId = Convert.ToInt32(message.Id);
                Request request = db.Requests.FirstOrDefault(req => req.RequestId == msgId);

                if (request != null)
                {
                    request.Description = message.Inputdata;
                    request.RequestStatus = false;
                    request.AcceptStatus = true;
                    db.SaveChanges();

                    return RedirectToAction("BookingRequest");

                }
            }
            return View();
        }

        public ActionResult MyServices()
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {
                string mngrId = Session["UserId"].ToString();
                List<Service> services = db.Services.Where(service => service.ManagerId == mngrId).ToList();
                return View(services);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        public ActionResult CompleteService(int serviceId)
        {
            if (Session["UserId"] != null && Session["UserRole"].ToString() == "manager")
            {

                try
                {
                    Service completeService = db.Services.FirstOrDefault(service => service.ServiceId == serviceId);
                    completeService.ServiceEndDate = System.DateTime.Now;
                    completeService.Description = "Completed";
                    completeService.ServiceStatus = true;
                    db.SaveChanges();
                    return RedirectToAction("MyServices", "Manager");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult IsTuckExixts(string truckNumber)
        {
            List<Truck> trucks = db.Trucks.Where(trk => trk.TruckNumber.ToLower() == truckNumber.ToLower()).ToList();
            bool isExist = trucks.Count()!=0?true:false;
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
    }
}