﻿using System;
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
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Answer = "No"},
                new Question { Title = "This is another question",   Type = "Yes/No", DateModified = DateTime.Parse("2004-02-12"), CompliantAnswer = "Yes", Answer = "No"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Answer = "Yes"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Answer = "No"},
                new Question { Title = "This is a question",     Type = "Yes/No", DateModified = DateTime.Parse("1995-03-11"), CompliantAnswer = "No", Answer = "No"}
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();


            var surveys = new List<Survey>
            {
                new Survey { Approved = "No", Manager = "Hugh O'Connor", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateStart = DateTime.Parse("1995-03-11"), DateEnd = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>() },
                new Survey { Approved = "No", Manager = "Olivia Wilde", Title = "This is a survey",  DateAdded = DateTime.Parse("1995-03-11"), DateStart = DateTime.Parse("1995-03-11"), DateEnd = DateTime.Parse("1995-03-11"), Month = "June", Submitted = "NO", Questions = new List<Question>() }
            };
            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();

            surveys[0].Questions.Add(questions[0]);
            surveys[0].Questions.Add(questions[1]);
            context.SaveChanges();

            var users = new List<User>
            {
                new User { Password = "password", Name = "Hugh O'Connor", EmailAddress = "joe@ipsi.ie",  UserType = "Manager" },
                new User { Password = "password", Name = "Olivia Wilde", EmailAddress = "joe@ipsi.ie",  UserType = "Manager" },
                new User { Password = "password", Name = "Sinead Durnin", EmailAddress = "joe@ipsi.ie",  UserType = "RandC" }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}