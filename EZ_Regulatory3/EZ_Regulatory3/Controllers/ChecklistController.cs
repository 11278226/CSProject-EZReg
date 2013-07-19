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
    public class ChecklistController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /Checklist/

        public ViewResult Index()
        {
            List<User> users = db.Users
                .Where(i => i.SurveyAnswers.Count() != 0)
                .ToList();
            List<Survey> surveys = db.Surveys.ToList();
            List<Survey> filteredList = new List<Survey>();

            foreach (Survey s in surveys)
            {
                foreach (User u in users) 
                {
                    foreach (SurveyAnswer sa in u.SurveyAnswers)
                    {
                        if (s.ID == sa.SurveyID)
                        {
                            filteredList.Add(s);
                        }
                    }
                }
                
            }

            return View(filteredList);
        }

        public ViewResult ViewChecklist(int id, string[] selectedQuestions, string[] selectedAnswers)
        {
            Survey survey = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            var questionlist = survey.Questions;

            SurveyAnswer surveyanswer = db.SurveyAnswers
                .Include(i => i.Answers)
                .Where(i => i.SurveyID == id)
                .Single();
            var answerlist = surveyanswer.Answers;
            return View(new Tuple<List<Answer>, List<Question>>(answerlist.ToList(), questionlist.ToList()));
        }

        [HttpPost]
        public ActionResult ViewChecklist(int id, FormCollection formCollection, string[] selectedQuestions, string[] selectedAnswers)
        {
            var surveyAnswerToUpdate = db.SurveyAnswers
                .Where(i => i.ID == id)
                .Single();
            if (TryUpdateModel(surveyAnswerToUpdate, "", null, new string[] { "Answers" }))
            {
                try
                {

                    UpdateSurveyAnswer(selectedQuestions, surveyAnswerToUpdate);

                    db.Entry(surveyAnswerToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAnswerData(surveyAnswerToUpdate);
            return View(surveyAnswerToUpdate);
        }

        private void PopulateAnswerData(SurveyAnswer survey)
        {
            var allAnswers = db.Answers;
            var surveyAnswers = new HashSet<int>(survey.Answers.Select(c => c.QuestionID));
            //var viewModel = new List<AssignedAnswerData>();
            foreach (var question in allAnswers)
            {
                //viewModel.Add(new AssignedQuestionData
                //{
                //    QuestionID = question.QuestionID,
                //    Title = question.Title,
                //    Assigned = surveyQuestions.Contains(question.QuestionID)
                //});
            }
           // ViewBag.Questions = viewModel;
        }

        private void UpdateSurveyAnswer(string[] selectedAnswers, SurveyAnswer surveyAnswerToUpdate)
        {
            if (selectedAnswers == null)
            {
                surveyAnswerToUpdate.Answers = new List<Answer>();
                return;
            }

            var selectedAnswersHS = new HashSet<string>(selectedAnswers);
            var surveyQuestions = new HashSet<int>
                (surveyAnswerToUpdate.Answers.Select(c => c.QuestionID));
            foreach (var answer in db.Answers)
            {
                if (selectedAnswersHS.Contains(answer.QuestionID.ToString()))
                {
                    if (!surveyQuestions.Contains(answer.QuestionID))
                    {
                        surveyAnswerToUpdate.Answers.Add(answer);
                    }
                }
                else
                {
                    if (surveyQuestions.Contains(answer.QuestionID))
                    {
                        surveyAnswerToUpdate.Answers.Remove(answer);
                    }
                }
            }
        }

        //
        // GET: /Checklist/Details/5

        public ViewResult Details(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // GET: /Checklist/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Checklist/Create

        [HttpPost]
        public ActionResult Create(Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(survey);
        }
        
        //
        // GET: /Checklist/Edit/5
 
        public ActionResult Edit(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // POST: /Checklist/Edit/5

        [HttpPost]
        public ActionResult Edit(Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(survey);
        }

        //
        // GET: /Checklist/Delete/5
 
        public ActionResult Delete(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // POST: /Checklist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
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