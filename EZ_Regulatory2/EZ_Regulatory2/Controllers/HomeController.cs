using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory2.Models;

namespace EZ_Regulatory2.Controllers
{
    public class HomeController : Controller
    {

        private MyModelDBContext db = new MyModelDBContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Check the status of the Monthly Checklists here.";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach(Survey s in surveys) {
                if(s.Month.ToUpper() == "JULY") {
                    filteredList.Add(s);
                } else if (s.Approved.ToUpper() == "NO")
                {
                    filteredList.Add(s);
                }
            }
            return View(filteredList);
        }

        public ActionResult Surveys()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Your admin page.";

            return View();
        }
    }
}
