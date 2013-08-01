using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;
using EZ_Regulatory3.ViewModels;
using EZ_Regulatory3.CSV;
using System.Data.OleDb;

namespace EZ_Regulatory3.Controllers
{
    public class ManagerChecklistController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /ManagerChecklist/

        public ViewResult Index()
        {
            List<Survey> surveys = db.Surveys
            .Where(i => i.SurveyAnswers.Count() != 0)
                .ToList();
            //List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();

            foreach (Survey s in surveys)
            {
                
                if (s.DateStart.CompareTo(System.DateTime.Now) < 1)
                {
                    filteredList.Add(s);
           
                }
            }

            return View(filteredList);
        }

        //
        // GET: /ManagerChecklist/Details/5

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

        //
        // GET: /ManagerChecklist/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManagerChecklist/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /ManagerChecklist/Edit/5

        public ActionResult Edit(int id)
        {
            SurveyAnswer surveyAnswer = db.SurveyAnswers.ToList()
                .Where(i => i.ID == id)
                .Single();

            User user = db.Users.Find(surveyAnswer.UserID);
            ViewBag.UserName = user.Name;
            Survey survey = db.Surveys.Find(surveyAnswer.SurveyID);
            ViewBag.Questions = survey.Questions.ToList();
            populateQuestionsAndAnswers(surveyAnswer);
            return View(surveyAnswer);
        }

        private void populateQuestionsAndAnswers(SurveyAnswer surveyAnswer)
        {
            var viewModel = new List<QuestionCommentsAndAnswers>();
            foreach (var answers in surveyAnswer.Answers)
            {
                Question thisQuestion = db.Questions.Find(answers.QuestionID);
                viewModel.Add(new QuestionCommentsAndAnswers
                {
                    Question = thisQuestion.Title,
                    QuestionID = answers.QuestionID,
                    QuestionAnswer = answers.QuestionAnswer,
                    QuestionComment = answers.QuestionComment
                });
            }
            ViewBag.Answers = viewModel;
        }

        //
        // POST: /ManagerChecklist/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, string inputButton, FormCollection formCollection, string[] questionAnswers, string[] questionComments)
        {
            //formCollection.
            SurveyAnswer surveyAnswer = db.SurveyAnswers.ToList()
                .Where(i => i.ID == id)
                .Single();

            UpdateSurveyAnswers(surveyAnswer, questionAnswers, questionComments);

            if (inputButton == "Submit")
                surveyAnswer.Submitted = "Yes";


            db.Entry(surveyAnswer).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        private void UpdateSurveyAnswers(SurveyAnswer surveyAnswer, string[] questionAnswers, string[] questionComments)
        {
            for (int i = 0; i < surveyAnswer.Answers.Count(); i++)
            {
                Answer ans = surveyAnswer.Answers.ElementAt(i);
                ans.QuestionAnswer = questionAnswers[i];
                ans.QuestionComment = questionComments[i];
                db.Entry(ans).State = EntityState.Modified;
                db.SaveChanges();
            }


        }

        //
        // GET: /ManagerChecklist/Delete/5

        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /ManagerChecklist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
            dt.Columns.Add(cl); //--Add 'No' Column in datatable 
            cl = new DataColumn(" "); //-- Create column 'Name'
            dt.Columns.Add(cl);
            cl = new DataColumn("Month:"); //-- Create column 'Name'
            dt.Columns.Add(cl);
            cl = new DataColumn(survey.Month); //-- Create column 'Name'
            dt.Columns.Add(cl); //--Add 'Name' Column in datatable
            cl = new DataColumn("  "); //-- Create column 'Name'
            dt.Columns.Add(cl); //--Add 'Name' Column in datatable
            cl = new DataColumn("User:"); //-- Create column 'Name'
            dt.Columns.Add(cl);
            cl = new DataColumn(user.Name); //-- Create column 'Name'
            dt.Columns.Add(cl);

            DataRow dr = dt.NewRow(); //-- get new row for datatable.
            dr[0] = "You or people reporting to you have made commitments to adhering to company policy on Risk and Compliance, including providing this checklist to Risk and Compliance by the due date"; //-- assign value for 'No' column
            dt.Rows.Add(dr);

            dr = dt.NewRow(); //-- get new row for datatable
            dt.Rows.Add(dr);

            dr = dt.NewRow(); //-- get new row for datatable.
            dr[0] = "Nature of Question"; //-- assign value for 'No' column
            dr[1] = "Question Number:";
            dr[2] = "Question";
            dr[3] = "Did you adhere to this requirement?";
            dr[4] = "Where you answer No please explain";
            dt.Rows.Add(dr);


            //-- add first row in datatable
            dr = dt.NewRow(); 
            dt.Rows.Add(dr);  // Add row in datatable


            //-- add second row in datatable
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}