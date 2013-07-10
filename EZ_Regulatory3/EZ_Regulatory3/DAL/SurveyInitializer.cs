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
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No"},
                new Question { Title = "This is another question",   Type = "Yes/No", DateModified = DateTime.Parse("2004-02-12"), CompliantAnswer = "Yes" }
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();


            var surveys = new List<Survey>
            {
                new Survey { Approved = "No", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateModified = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>() },
                new Survey { Approved = "No", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateModified = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>() }
            };
            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            surveys[0].Questions.Add(questions[0]);
            surveys[0].Questions.Add(questions[1]);
            context.SaveChanges();
        }
    }
}