﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EZ_Regulatory3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to EZ Reg";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
