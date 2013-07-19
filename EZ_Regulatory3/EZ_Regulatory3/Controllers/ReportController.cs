using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;

namespace EZ_Regulatory3.Controllers
{ 
    public class ReportController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /Report/

        public ViewResult Index()
        {
            // This Month
            ViewBag.Message = "Check the status of the Monthly Checklists here.";
            DateTime dt = DateTime.Now;
            var x = dt.ToString("MMMM");
            ViewBag.Message = x;
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                if (s.Month.ToUpper() == dt.ToString("MMMM").ToUpper())
                {
                    foreach (User u in s.Users)
                    {
                        filteredList.Add(s);
                    }
                }
                
            }
            return View(filteredList);
        }

        public ViewResult ViewAll()
        {
            ViewBag.Message = "View All Surveys";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                foreach (User u in s.Users)
                    {
                        filteredList.Add(s);
                    }
                

            }
            
            return View(filteredList);
        }

        public ViewResult ViewUnapproved()
        {
            ViewBag.Message = "View Unapproved Surveys";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                foreach (User u in s.Users)
                {
                    if (s.Approved.ToUpper() == "NO")
                    {
                        filteredList.Add(s);
                    }
                }
            }
            return View(filteredList);
        }
    }
}