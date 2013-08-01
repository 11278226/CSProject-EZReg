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
using EZ_Regulatory3.CSV;

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
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "January", Value = "January" });
            items.Add(new SelectListItem { Text = "February", Value = "February" });
            items.Add(new SelectListItem { Text = "March", Value = "March" });
            items.Add(new SelectListItem { Text = "April", Value = "April" });
            items.Add(new SelectListItem { Text = "May", Value = "May" });
            items.Add(new SelectListItem { Text = "June", Value = "June" });
            items.Add(new SelectListItem { Text = "July", Value = "July" });
            items.Add(new SelectListItem { Text = "August", Value = "August" });
            items.Add(new SelectListItem { Text = "September", Value = "September" });
            items.Add(new SelectListItem { Text = "October", Value = "October" });
            items.Add(new SelectListItem { Text = "November", Value = "November" });
            items.Add(new SelectListItem { Text = "December", Value = "December" });

            ViewBag.Month = items;
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
            
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "January", Value = "January" });
            items.Add(new SelectListItem { Text = "February", Value = "February" });
            items.Add(new SelectListItem { Text = "March", Value = "March" });
            items.Add(new SelectListItem { Text = "April", Value = "April" });
            items.Add(new SelectListItem { Text = "May", Value = "May" });
            items.Add(new SelectListItem { Text = "June", Value = "June" });
            items.Add(new SelectListItem { Text = "July", Value = "July" });
            items.Add(new SelectListItem { Text = "August", Value = "August" });
            items.Add(new SelectListItem { Text = "September", Value = "September" });
            items.Add(new SelectListItem { Text = "October", Value = "October" });
            items.Add(new SelectListItem { Text = "November", Value = "November" });
            items.Add(new SelectListItem { Text = "December", Value = "December" });
            if (survey.Month == "January")
            {
                items.ElementAt(0).Selected = true;
            }
            else if (survey.Month == "February")
            {
                items.ElementAt(1).Selected = true;
            }
            else if (survey.Month == "March")
            {
                items.ElementAt(2).Selected = true;
            }
            else if (survey.Month == "April")
            {
                items.ElementAt(3).Selected = true;
            }
            else if (survey.Month == "May")
            {
                items.ElementAt(4).Selected = true;
            }
            else if (survey.Month == "June")
            {
                items.ElementAt(5).Selected = true;
            }
            else if (survey.Month == "July")
            {
                items.ElementAt(6).Selected = true;
            }
            else if (survey.Month == "August")
            {
                items.ElementAt(7).Selected = true;
            }
            else if (survey.Month == "September")
            {
                items.ElementAt(8).Selected = true;
            }
            else if (survey.Month == "October")
            {
                items.ElementAt(9).Selected = true;
            }
            else if (survey.Month == "November")
            {
                items.ElementAt(10).Selected = true;
            }
            else if (survey.Month == "December")
            {
                items.ElementAt(11).Selected = true;
            }
            ViewBag.Month = items;

            return View(survey);
        }

        //
        // GET: /Survey/Edit/5

        public ActionResult CreateFromThisChecklist(int id, string[] selectedQuestions, string[] selectedUsers)
        {
            Survey survey = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "January", Value = "January" });
            items.Add(new SelectListItem { Text = "February", Value = "February" });
            items.Add(new SelectListItem { Text = "March", Value = "March" });
            items.Add(new SelectListItem { Text = "April", Value = "April" });
            items.Add(new SelectListItem { Text = "May", Value = "May" });
            items.Add(new SelectListItem { Text = "June", Value = "June" });
            items.Add(new SelectListItem { Text = "July", Value = "July" });
            items.Add(new SelectListItem { Text = "August", Value = "August" });
            items.Add(new SelectListItem { Text = "September", Value = "September" });
            items.Add(new SelectListItem { Text = "October", Value = "October" });
            items.Add(new SelectListItem { Text = "November", Value = "November" });
            items.Add(new SelectListItem { Text = "December", Value = "December" });
            if (survey.Month == "January")
            {
                items.ElementAt(0).Selected = true;
            }
            else if (survey.Month == "February")
            {
                items.ElementAt(1).Selected = true;
            }
            else if (survey.Month == "March")
            {
                items.ElementAt(2).Selected = true;
            }
            else if (survey.Month == "April")
            {
                items.ElementAt(3).Selected = true;
            }
            else if (survey.Month == "May")
            {
                items.ElementAt(4).Selected = true;
            }
            else if (survey.Month == "June")
            {
                items.ElementAt(5).Selected = true;
            }
            else if (survey.Month == "July")
            {
                items.ElementAt(6).Selected = true;
            }
            else if (survey.Month == "August")
            {
                items.ElementAt(7).Selected = true;
            }
            else if (survey.Month == "September")
            {
                items.ElementAt(8).Selected = true;
            }
            else if (survey.Month == "October")
            {
                items.ElementAt(9).Selected = true;
            }
            else if (survey.Month == "November")
            {
                items.ElementAt(10).Selected = true;
            }
            else if (survey.Month == "December")
            {
                items.ElementAt(11).Selected = true;
            }
            ViewBag.Month = items;

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
        public ActionResult Edit(int id, FormCollection formCollection, string [] selectedQuestions, string [] selectedUsers)
        {
            var surveyToUpdate = db.Surveys
                .Include(i => i.Questions)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(surveyToUpdate, "", null, new string[] { "Questions", "Users", "Answers" }))
            {
                try
                {
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

        [HttpPost]
        public ActionResult CreateFromThisChecklist(Survey survey)
        {
            Survey newSurvey = new Survey { Title = survey.Title, DateStart = survey.DateStart, DateEnd = survey.DateEnd, Month = survey.Month, Questions = new List<Question>(), Users = new List<User>() };
            if (ModelState.IsValid)
            {
                db.Surveys.Add(newSurvey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(survey);
        }

        private void UpdateSurveyQuestionsAndUsers(string[] selectedQuestions, string[] selectedUsers, Survey surveyToUpdate)
        {
            if (selectedQuestions == null && selectedUsers == null)
            {
                surveyToUpdate.Questions = new List<Question>();
                surveyToUpdate.Users = new List<User>();
                surveyToUpdate.SurveyAnswers = new List<SurveyAnswer>();
                var allSurveyAnswers = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID)
                                .ToList();
                for (int x = allSurveyAnswers.Count() - 1; x >= 0; x--)
                {
                    SurveyAnswer surveyAnswers = allSurveyAnswers.ElementAt(x);
                    for (int y = surveyAnswers.Answers.Count() - 1; y >= 0; y--)
                    {
                        Answer answer = surveyAnswers.Answers.ElementAt(y);
                        db.Answers.Remove(answer);
                    }
                    db.SurveyAnswers.Remove(surveyAnswers);
                }
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
                            List<Answer> answers = new List<Answer>();
                            //foreach (string questionID in selectedQuestions)
                            //{
                            //    answers.Add(new Answer { QuestionID = Convert.ToInt32(questionID) });
                            //}
                            SurveyAnswer newSurveyAnswer = new SurveyAnswer { UserID = user.ID, SurveyID = surveyToUpdate.ID, Answers = answers };
                            surveyToUpdate.SurveyAnswers.Add(newSurveyAnswer);
                        }
                    }
                    else
                    {
                        if (surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Remove(user);

                            var surveyAnswersExist = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID && i.UserID == user.ID)
                                .Single();

                            for (int x = surveyAnswersExist.Answers.Count() - 1; x >= 0; x--)
                            {
                                Answer answer = surveyAnswersExist.Answers.ElementAt(x);
                                db.Answers.Remove(answer);
                            }
                            db.SurveyAnswers.Remove(surveyAnswersExist);
                        }
                    }
                }
            }
            else if (selectedUsers == null)
            {
                surveyToUpdate.Users = new List<User>();
                surveyToUpdate.SurveyAnswers = new List<SurveyAnswer>();
                var allSurveyAnswers = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID)
                                .ToList();
                for (int x = allSurveyAnswers.Count() - 1; x >= 0; x--)
                {
                    SurveyAnswer surveyAnswers = allSurveyAnswers.ElementAt(x);
                    for (int y = surveyAnswers.Answers.Count() - 1; y >= 0; y--)
                    {
                        Answer answer = surveyAnswers.Answers.ElementAt(y);
                        db.Answers.Remove(answer);
                    }
                    db.SurveyAnswers.Remove(surveyAnswers);
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
                            var surveyAnswersExist = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID)
                                .ToList();
                            foreach (SurveyAnswer surveyanswer in surveyToUpdate.SurveyAnswers)
                            {
                                surveyanswer.Answers.Add(new Answer { QuestionID = Convert.ToInt32(question.QuestionID) });
                            }
                            surveyToUpdate.Questions.Add(question);

                        }
                    }
                    else
                    {
                        if (surveyQuestions.Contains(question.QuestionID))
                        {
                            foreach (SurveyAnswer surveyanswer in surveyToUpdate.SurveyAnswers)
                            {
                                foreach (Answer answer in surveyanswer.Answers)
                                {
                                    if (answer.QuestionID == question.QuestionID)
                                    {
                                        surveyanswer.Answers.Remove(answer);
                                    }
                                }
                            }
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
                            var surveyAnswersExist = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID)
                                .ToList();
                            foreach (SurveyAnswer surveyanswer in surveyToUpdate.SurveyAnswers)
                            {
                                surveyanswer.Answers.Add(new Answer { QuestionID = Convert.ToInt32(question.QuestionID) });
                            }
                            surveyToUpdate.Questions.Add(question);
                        }
                    }
                    else
                    {
                        if (surveyQuestions.Contains(question.QuestionID))
                        {
                            foreach (SurveyAnswer surveyanswer in surveyToUpdate.SurveyAnswers)
                            {
                                for (int x = surveyanswer.Answers.Count() - 1; x >= 0; x--)
                                {
                                    Answer answer = surveyanswer.Answers.ElementAt(x);
                                    if (answer.QuestionID == question.QuestionID)
                                    {
                                        surveyanswer.Answers.Remove(answer);
                                    }
                                }
                            }
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
                            List<Answer> answers = new List<Answer>();
                            foreach (string questionID in selectedQuestions)
                            {
                                answers.Add(new Answer { QuestionID = Convert.ToInt32(questionID) });
                            }
                            SurveyAnswer newSurveyAnswer = new SurveyAnswer { UserID = user.ID, SurveyID = surveyToUpdate.ID, Answers = answers };
                            surveyToUpdate.SurveyAnswers.Add(newSurveyAnswer);
                        }
                    }
                    else
                    {
                        if (surveyUsers.Contains(user.ID))
                        {
                            surveyToUpdate.Users.Remove(user);
                            var surveyAnswersExist = db.SurveyAnswers
                                .Where(i => i.SurveyID == surveyToUpdate.ID && i.UserID == user.ID)
                                .Single();



                            for (int x = surveyAnswersExist.Answers.Count() - 1; x >= 0; x--)
                            {
                                Answer answer = surveyAnswersExist.Answers.ElementAt(x);
                                db.Answers.Remove(answer);
                            }
                            db.SurveyAnswers.Remove(surveyAnswersExist);

                            //surveyToUpdate.SurveyAnswers.Remove(surveyAnswersExist);

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
            survey.SurveyAnswers = new List<SurveyAnswer>();
            var allSurveyAnswers = db.SurveyAnswers
                            .Where(i => i.SurveyID == survey.ID)
                            .ToList();
            for (int x = allSurveyAnswers.Count() - 1; x >= 0; x--)
            {
                SurveyAnswer surveyAnswers = allSurveyAnswers.ElementAt(x);
                for (int y = surveyAnswers.Answers.Count() - 1; y >= 0; y--)
                {
                    Answer answer = surveyAnswers.Answers.ElementAt(y);
                    db.Answers.Remove(answer);
                }
                db.SurveyAnswers.Remove(surveyAnswers);
            }
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