using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;
using EZ_Regulatory3.ViewModels;

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
            //List<Survey> filteredList = new List<Survey>();

            //foreach (Survey s in surveys)
            //{
            //    foreach (User u in users) 
            //    {
            //        foreach (SurveyAnswer sa in u.SurveyAnswers)
            //        {
            //            if (s.ID == sa.SurveyID)
            //            {
            //                filteredList.Add(s);
            //           }
            //        }
            //    }
            //    
            //}

            return View(surveys);
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
        public ActionResult Edit(int id, string[] questionAnswers, string[] questionComments)
        {
            SurveyAnswer surveyAnswer = db.SurveyAnswers.ToList()
                .Where(i => i.ID == id)
                .Single();

            UpdateSurveyAnswers(surveyAnswer, questionAnswers, questionComments);

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}