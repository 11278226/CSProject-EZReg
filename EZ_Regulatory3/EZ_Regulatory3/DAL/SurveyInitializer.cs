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
                new Question { Title = "This is a question 1",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is another question",   Type = "Yes/No", DateModified = DateTime.Parse("2004-02-12"), CompliantAnswer = "Yes", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 2",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 3",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 4",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 5",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 6",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()},
                new Question { Title = "This is a question 7",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Surveys = new List<Survey>()}
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();


            var surveys = new List<Survey>
            {
                new Survey { Approved = "No", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateStart = DateTime.Parse("1995-03-11"), DateEnd = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>(), Users = new List<User>() },
                new Survey { Approved = "No", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateStart = DateTime.Parse("1995-03-11"), DateEnd = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>(), Users = new List<User>() }
            };
            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            surveys[0].Questions.Add(questions[0]);
            surveys[0].Questions.Add(questions[1]);
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
                new Answer { QuestionAnswer = "Yes",   QuestionID = 1, QuestionComment = "This is a comment"},
                new Answer { QuestionAnswer = "No", QuestionID = 2}
               
            };
            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();

            var surveyanswers = new List<SurveyAnswer>
            {
                new SurveyAnswer { SurveyID = 1, UserID = 1,  Answers = new List<Answer>()}
               
            };
            surveyanswers.ForEach(s => context.SurveyAnswers.Add(s));
            context.SaveChanges();

            surveys[0].Users.Add(users[0]);
            context.SaveChanges();

            surveyanswers[0].Answers.Add(answers[0]);
            surveyanswers[0].Answers.Add(answers[1]);
            context.SaveChanges();

            surveys[0].SurveyAnswers.Add(surveyanswers[0]);
            context.SaveChanges();
        }
    }
}