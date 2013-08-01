using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using EZ_Regulatory3.Models;

namespace EZ_Regulatory3.DAL
{
    public class SurveyInitializer : DropCreateDatabaseIfModelChanges<SurveyDBContext>
    {
        protected override void Seed(SurveyDBContext context)
        {
            var questions = new List<Question>
            {
                new Question { Title = "Have you acted honestly & fairly and professionally and with due skill, careand diligence in the best interests of the client and the integrity of the market?",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Have you recklessly, negligently or deliberately mislead a client as to the real or perceived advantages or disadvantages of any service.",   Type = "Yes/No", DateModified = DateTime.Parse("2004-02-12"), CompliantAnswer = "Yes", Surveys = new List<Survey>()},
                new Question { Title = "Have you sought from clients information relevant to the product or service requested",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Have you made full disclosure of all relevant material information, including all charges, in a way that seeks to inform the client",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Have you sought to avoid conflicts of interest",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Have you corrected errors and handled complaints speedily, efficiently and fairly",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Has the error log been updated for all errors occurring during the month?",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Has compliance been notified immediately of all material financial errors (>€100,000)",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Are all staff are aware of the Conflicts of Interest / Inducements / Gifts policy",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Can you confirm that you have not paid or received any fee/ benefit in relation to services provided to clients (excluding fees paid by/to persons from/to IPSI in line with the contractual provision of the service to clients)",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "Can you confirm that All staff are aware of the Personal Account Dealing Transaction procedure",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "All handwritten/typed notes only record facts and do not contain any derogatory remarks about individuals.",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()}
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();


            var surveys = new List<Survey>
            {
                new Survey {  Title = "Actuarial Operations Breaches Checklist", DateStart = DateTime.Parse("2013-07-11"), DateEnd = DateTime.Parse("2013-07-29"), Month = "July", Questions = new List<Question>(), Users = new List<User>() },
                new Survey {  Title = "CRM Breaches Checklist", DateStart = DateTime.Parse("2013-07-11"), DateEnd = DateTime.Parse("2013-07-29"), Month = "July", Questions = new List<Question>(), Users = new List<User>() },
                new Survey {  Title = "Actuarial Operations Breaches Checklist", DateStart = DateTime.Parse("2013-08-05"), DateEnd = DateTime.Parse("2013-08-17"), Month = "August", Questions = new List<Question>(), Users = new List<User>() },
                new Survey {  Title = "CRM Breaches Checklist", DateStart = DateTime.Parse("2013-07-28"), DateEnd = DateTime.Parse("2013-08-11"), Month = "August", Questions = new List<Question>(), Users = new List<User>() },           
                new Survey {  Title = "Actuarial Operations Breaches Checklist", DateStart = DateTime.Parse("2013-09-01"), DateEnd = DateTime.Parse("2013-09-11"), Month = "September", Questions = new List<Question>(), Users = new List<User>() },        
                new Survey {  Title = "CRM Breaches Checklist", DateStart = DateTime.Parse("2013-09-01"), DateEnd = DateTime.Parse("2013-09-11"), Month = "September", Questions = new List<Question>(), Users = new List<User>() }            

            };
            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            surveys[0].Questions.Add(questions[0]);
            surveys[0].Questions.Add(questions[1]);

            surveys[1].Questions.Add(questions[2]);
            surveys[1].Questions.Add(questions[3]);

            surveys[2].Questions.Add(questions[4]);
            surveys[2].Questions.Add(questions[5]);

            surveys[3].Questions.Add(questions[6]);
            surveys[3].Questions.Add(questions[7]);

            surveys[4].Questions.Add(questions[8]);
            surveys[4].Questions.Add(questions[9]);

            surveys[5].Questions.Add(questions[1]);
            surveys[5].Questions.Add(questions[2]);
            context.SaveChanges();

            var users = new List<User>
            {
                new User { Password = "password", Name = "Hugh O'Connor", EmailAddress = "joe@ipsi.ie",  UserType = "Manager", Surveys = new List<Survey>() },
                new User { Password = "password", Name = "Olivia Wilde", EmailAddress = "joe@ipsi.ie",  UserType = "Manager", Surveys = new List<Survey>() },
                new User { Password = "password", Name = "Sinead Durnin", EmailAddress = "joe@ipsi.ie",  UserType = "RandC", Surveys = new List<Survey>() }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>
            {
                new Answer { QuestionAnswer = "Yes", QuestionID = 1, QuestionComment = "This is a comment"},
                new Answer { QuestionAnswer = "No", QuestionID = 2},
                new Answer { QuestionAnswer = "No",   QuestionID = 1, QuestionComment = "This is a comment"},
                new Answer { QuestionAnswer = "No", QuestionID = 2},
                //new Answer { QuestionAnswer = "No",   QuestionID = 3, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 4},
                //new Answer { QuestionAnswer = "No",   QuestionID = 3, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 4},
                //new Answer { QuestionAnswer = "No",   QuestionID = 5, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 6},
                //new Answer { QuestionAnswer = "No",   QuestionID = 5, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 6},
                //new Answer { QuestionAnswer = "No",   QuestionID = 7, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 8},
                //new Answer { QuestionAnswer = "No",   QuestionID = 7, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 8},
                //new Answer { QuestionAnswer = "No",   QuestionID = 2, QuestionComment = "This is a comment"},
                //new Answer { QuestionAnswer = "No", QuestionID = 3},
               
            };
            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();

            var surveyanswers = new List<SurveyAnswer>
            {
                new SurveyAnswer { SurveyID = 1, UserID = 1, Submitted = "Yes", Approved = "Yes", Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 3, UserID = 1, Submitted = "Yes", Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 5, UserID = 1, Answers = new List<Answer>()},
                new SurveyAnswer { SurveyID = 1, UserID = 3, Submitted = "Yes", Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 3, UserID = 3, Submitted = "Yes", Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 5, UserID = 3, Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 2, UserID = 2, Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 4, UserID = 2, Answers = new List<Answer>()},
                //new SurveyAnswer { SurveyID = 6, UserID = 2, Answers = new List<Answer>()},
            };
            surveyanswers.ForEach(s => context.SurveyAnswers.Add(s));
            context.SaveChanges();

            surveys[0].Users.Add(users[0]);
            surveys[0].Users.Add(users[2]);
            //surveys[2].Users.Add(users[0]);
            //surveys[2].Users.Add(users[2]);
            //surveys[4].Users.Add(users[0]);
            //surveys[4].Users.Add(users[2]);
            //surveys[1].Users.Add(users[1]);
            //surveys[3].Users.Add(users[1]);
            //surveys[5].Users.Add(users[1]);
            context.SaveChanges();

            surveyanswers[0].Answers.Add(answers[0]);
            surveyanswers[0].Answers.Add(answers[1]);
            //surveyanswers[1].Answers.Add(answers[6]);
            //surveyanswers[1].Answers.Add(answers[7]);
            //surveyanswers[2].Answers.Add(answers[12]);
            //surveyanswers[2].Answers.Add(answers[13]);
            surveyanswers[1].Answers.Add(answers[2]);
            surveyanswers[1].Answers.Add(answers[3]);
            //surveyanswers[4].Answers.Add(answers[8]);
            //surveyanswers[4].Answers.Add(answers[9]);
            //surveyanswers[5].Answers.Add(answers[14]);
            //surveyanswers[5].Answers.Add(answers[15]);
            //surveyanswers[6].Answers.Add(answers[4]);
            //surveyanswers[6].Answers.Add(answers[5]);
            //surveyanswers[7].Answers.Add(answers[10]);
            //surveyanswers[7].Answers.Add(answers[11]);
            //surveyanswers[8].Answers.Add(answers[16]);
            //surveyanswers[8].Answers.Add(answers[17]);
            context.SaveChanges();

            surveys[0].SurveyAnswers.Add(surveyanswers[0]);
            surveys[0].SurveyAnswers.Add(surveyanswers[1]);
            //surveys[2].SurveyAnswers.Add(surveyanswers[1]);
            //surveys[2].SurveyAnswers.Add(surveyanswers[4]);
            ///surveys[4].SurveyAnswers.Add(surveyanswers[2]);
            //surveys[4].SurveyAnswers.Add(surveyanswers[5]);
            //surveys[1].SurveyAnswers.Add(surveyanswers[6]);
            //surveys[3].SurveyAnswers.Add(surveyanswers[7]);
            //surveys[5].SurveyAnswers.Add(surveyanswers[8]);
            context.SaveChanges();
        }
    }
}