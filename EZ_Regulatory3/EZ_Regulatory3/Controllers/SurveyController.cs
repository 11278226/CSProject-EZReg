using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;
using EZ_Regulatory3.DAL;
using EZ_Regulatory3.ViewModels;

namespace EZ_Regulatory3.Controllers
{ 
    public class SurveyController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /Survey/

        public ViewResult Index()
        {
            return View(db.Surveys.ToList());
        }

        //
        // GET: /Survey/Details/5

        public ViewResult Details(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // GET: /Survey/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Survey/Create

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
        // GET: /Survey/Edit/5

        public ActionResult Edit(int id, string[] selectedQuestions)
        {
            Survey survey = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedQuestionData(survey);
            return View(survey);
        }

        private void PopulateAssignedQuestionData(Survey survey)
        {
            var allQuestions = db.Questions;
            var surveyQuestions = new HashSet<int>(survey.Questions.Select(c => c.QuestionID));
            var viewModel = new List<AssignedQuestionData>();
            foreach (var question in allQuestions)
            {
                viewModel.Add(new AssignedQuestionData
                {
                    QuestionID = question.QuestionID,
                    Title = question.Title,
                    Assigned = surveyQuestions.Contains(question.QuestionID)
                });
            }
            ViewBag.Questions = viewModel;
        }

        private void PopulateActiveAssignedQuestionData(Survey survey)
        {
            var allQuestions = db.Questions;
            var surveyQuestions = new HashSet<int>(survey.Questions.Select(c => c.QuestionID));
            var viewModel = new List<AssignedQuestionData>();
            foreach (var question in allQuestions)
            {
                if (surveyQuestions.Contains(question.QuestionID))
                {
                    viewModel.Add(new AssignedQuestionData
                    {
                        QuestionID = question.QuestionID,
                        Title = question.Title,
                        Assigned = surveyQuestions.Contains(question.QuestionID)
                    });
                }
            }
            ViewBag.Questions = viewModel;
        }

        //
        // POST: /Instructor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedQuestions)
        {
            var surveyToUpdate = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            if (TryUpdateModel(surveyToUpdate, "", null, new string[] { "Questions" }))
            {
                try
                {
                    //if (String.IsNullOrWhiteSpace(surveyToUpdate.OfficeAssignment.Location))
                    //{
                    //    surveyToUpdate.OfficeAssignment = null;
                    //}

                    UpdateSurveyQuestions(selectedQuestions, surveyToUpdate);

                    db.Entry(surveyToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (add a variable name after DataException)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedQuestionData(surveyToUpdate);
            return View(surveyToUpdate);
        }

        private void UpdateSurveyQuestions(string[] selectedQuestions, Survey surveyToUpdate)
        {
            if (selectedQuestions == null)
            {
                surveyToUpdate.Questions = new List<Question>();
                return;
            }

            var selectedQuestionsHS = new HashSet<string>(selectedQuestions);
            var surveyQuestions = new HashSet<int>
                (surveyToUpdate.Questions.Select(c => c.QuestionID));
            foreach (var question in db.Questions)
            {
                if (selectedQuestionsHS.Contains(question.QuestionID.ToString()))
                {
                    if (!surveyQuestions.Contains(question.QuestionID))
                    {
                        surveyToUpdate.Questions.Add(question);
                    }
                }
                else
                {
                    if (surveyQuestions.Contains(question.QuestionID))
                    {
                        surveyToUpdate.Questions.Remove(question);
                    }
                }
            }
        }

        //
        // GET: /Survey/Delete/5
 
        public ActionResult Delete(int id)
        {
            Survey survey = db.Surveys.Find(id);
            return View(survey);
        }

        //
        // POST: /Survey/Delete/5

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

        public ViewResult View(int id, string[] selectedQuestions)
        {
            Survey survey = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            PopulateActiveAssignedQuestionData(survey);
            return View(survey);
        }
    }
}