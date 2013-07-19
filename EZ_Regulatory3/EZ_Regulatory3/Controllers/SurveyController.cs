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

        public ActionResult Edit(int id, string[] selectedQuestions, string[] selectedUsers)
        {
            Survey survey = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedQuestionData(survey);
            PopulateAssignedUserData(survey);
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

        private void PopulateAssignedUserData(Survey survey)
        {
            var allUsers = db.Users;
            var surveyUsers = new HashSet<int>(survey.Users.Select(c => c.ID));
            var viewModel = new List<AssignedUserData>();
            foreach (var user in allUsers)
            {
                viewModel.Add(new AssignedUserData
                {
                    UserID = user.ID,
                    Name = user.Name,
                    Assigned = surveyUsers.Contains(user.ID)
                });
            }
            ViewBag.Users = viewModel;
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

        private void PopulateActiveAssignedUserData(Survey survey)
        {
            var allUsers = db.Users;
            var surveyUsers = new HashSet<int>(survey.Users.Select(c => c.ID));
            var viewModel = new List<AssignedUserData>();
            foreach (var user in allUsers)
            {
                if (surveyUsers.Contains(user.ID))
                {
                    viewModel.Add(new AssignedUserData
                    {
                        UserID = user.ID,
                        Name = user.Name,
                        Assigned = surveyUsers.Contains(user.ID)
                    });
                }
            }
            ViewBag.Users = viewModel;
        }

        //
        // POST: /Instructor/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedQuestions, string[] selectedUsers)
        {
            var surveyToUpdate = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();
            if (TryUpdateModel(surveyToUpdate, "", null, new string[] { "Questions","Users" }))
            {
                try
                {
                    //if (String.IsNullOrWhiteSpace(surveyToUpdate.OfficeAssignment.Location))
                    //{
                    //    surveyToUpdate.OfficeAssignment = null;
                    //}

                    UpdateSurveyQuestionsAndUsers(selectedQuestions, selectedUsers, surveyToUpdate);

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
            PopulateAssignedUserData(surveyToUpdate);
            return View(surveyToUpdate);
        }

        private void UpdateSurveyQuestionsAndUsers(string[] selectedQuestions, string[] selectedUsers, Survey surveyToUpdate)
        {
            if (selectedQuestions == null && selectedUsers == null)
            {
                surveyToUpdate.Questions = new List<Question>();
                surveyToUpdate.Users = new List<User>();
                return;
            }
            else if (selectedQuestions == null)
            {
                surveyToUpdate.Questions = new List<Question>();
                var selectedUsersHS = new HashSet<string>(selectedUsers);
                var surveyUsers = new HashSet<int>
                    (surveyToUpdate.Users.Select(c => c.ID));
                foreach (var user in db.Users)
                {
                    if (selectedUsersHS.Contains(user.ID.ToString()))
                    {
                        if (!surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Add(user);
                        }
                    }
                    else
                    {
                        if (surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Remove(user);
                        }
                    }
                }
            }
            else if (selectedUsers == null)
            {
                surveyToUpdate.Users = new List<User>();
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
            else
            {
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
                var selectedUsersHS = new HashSet<string>(selectedUsers);
                var surveyUsers = new HashSet<int>
                    (surveyToUpdate.Users.Select(c => c.ID));
                foreach (var user in db.Users)
                {
                    if (selectedUsersHS.Contains(user.ID.ToString()))
                    {
                        if (!surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Add(user);
                        }
                    }
                    else
                    {
                        if (surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Remove(user);
                        }
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
            PopulateActiveAssignedUserData(survey);
            return View(survey);
        }
    }
}