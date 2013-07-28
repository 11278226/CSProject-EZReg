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
            ViewBag.currentMonth = dt.ToString("MMMM").ToUpper();
            return View(surveys);
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
            
            return View(surveys);
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
            return View(surveys);
        }

        public ViewResult Details(int id, int surveyid)
        {
            SurveyAnswer surveyanswer = db.SurveyAnswers.ToList()
                .Where(i => i.UserID == id && i.SurveyID == surveyid)
                .Single();

            User user = db.Users.Find(id);
            ViewBag.UserName = user.Name;
            Survey survey = db.Surveys.Find(surveyid);
            ViewBag.Questions = survey.Questions.ToList();

            return View(surveyanswer);
        }
    }
}