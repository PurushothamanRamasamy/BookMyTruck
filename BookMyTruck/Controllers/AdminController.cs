﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BookMyTruck.Controllers
{
    
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        [Route("{Admindashboard}")]
        public ActionResult Index()
        {
            List<Service> services = new List<Service>();
            return View(services);
        }
    }
}