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
            ViewBag.Message = "Check the status of the Monthly Checklists here.";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                if (s.Month.ToUpper() == "JULY")
                {
                    filteredList.Add(s);
                }
                else if (s.Approved.ToUpper() == "NO")
                {
                    filteredList.Add(s);
                }
            }
            return View(filteredList);
        }
    }
}