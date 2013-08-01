using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;
using EZ_Regulatory3.CSV;



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
            ViewBag.SurveyAnswers = db.SurveyAnswers.ToList();
             
            return View(surveys);
        }

        public ViewResult ViewAll()
        {
            ViewBag.Message = "View All Checklists";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                foreach (User u in s.Users)
                    {
                        filteredList.Add(s);
                    }
                

            }
            ViewBag.SurveyAnswers = db.SurveyAnswers.ToList();
            return View(surveys);
        }

        public ViewResult ViewUnapproved()
        {
            ViewBag.Message = "View Unapproved Checklists";
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();
            foreach (Survey s in surveys)
            {
                foreach (User u in s.Users)
                {
                    SurveyAnswer surveyanswer = db.SurveyAnswers.ToList()
                    .Where(i => i.UserID == u.ID && i.SurveyID == s.ID)
                    .Single();
                    if (surveyanswer.Approved.ToUpper() == "NO")
                    {
                        filteredList.Add(s);
                    }
                }
            }
            ViewBag.SurveyAnswers = db.SurveyAnswers.ToList();

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

        [HttpPost]
        public ActionResult Details(int id, int surveyid, Survey survey)
        {
            SurveyAnswer surveyAnswer = db.SurveyAnswers.ToList()
                .Where(i => i.UserID == id && i.SurveyID == surveyid)
                .Single();
            surveyAnswer.Approved = "Yes";

            db.Entry(surveyAnswer).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult CSV(int userID, int surveyID)
        {
            SurveyAnswer surveyAnswer = db.SurveyAnswers.ToList()
                .Where(i => i.SurveyID == surveyID && i.UserID == userID)
                .Single();

            Survey survey = db.Surveys.ToList()
                .Where(i => i.ID == surveyID)
                .Single();

            User user = db.Users.ToList()
                .Where(i => i.ID == userID)
                .Single();

            DataTable dt = new DataTable();
            DataColumn cl = new DataColumn(survey.Title);
            dt.Columns.Add(cl);
            cl = new DataColumn(" ");
            dt.Columns.Add(cl);
            cl = new DataColumn("Month:");
            dt.Columns.Add(cl);
            cl = new DataColumn(survey.Month);
            dt.Columns.Add(cl);
            cl = new DataColumn("  ");
            dt.Columns.Add(cl);
            cl = new DataColumn("User:");
            dt.Columns.Add(cl);
            cl = new DataColumn(user.Name);
            dt.Columns.Add(cl);

            DataRow dr = dt.NewRow();
            dr[0] = "You or people reporting to you have made commitments to adhering to company policy on Risk and Compliance, including providing this checklist to Risk and Compliance by the due date"; //-- assign value for 'No' column
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "Nature of Question";
            dr[1] = "Question Number:";
            dr[2] = "Question";
            dr[3] = "Did you adhere to this requirement?";
            dr[4] = "Where you answer No please explain";
            dt.Rows.Add(dr);


            //-- add first row in datatable
            dr = dt.NewRow();
            dt.Rows.Add(dr);


            //-- add other rows in datatable
            foreach (Answer ans in surveyAnswer.Answers)
            {
                Question question = db.Questions.Find(ans.QuestionID);
                dr = dt.NewRow();
                dr[0] = "";
                dr[1] = question.QuestionID;
                dr[2] = question.Title;
                dr[3] = ans.QuestionAnswer;
                dr[4] = ans.QuestionComment;
                dt.Rows.Add(dr);
            }
            return new CsvActionResult(dt) { FileDownloadName = survey.Title + "_" + user.Name + "_" + survey.Month + ".csv" };
        }
    }
}