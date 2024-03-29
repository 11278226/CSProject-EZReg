﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory3.Models;

namespace EZ_Regulatory3.Controllers
{ 
    public class QuestionController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /Question/

        public ViewResult Index()
        {
            return View(db.Questions.ToList());
        }

        //
        // GET: /Question/Details/5

        public ViewResult Details(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Yes/No", Value = "Yes/No" });

            ViewBag.Type = items;
            return View();
        } 

        //
        // POST: /Question/Create

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(question);
        }
        
        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            Question question = db.Questions.Find(id);
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Yes/No", Value = "Yes/No" });

            if (question.Type == "Yes/No")
                items.ElementAt(0).Selected = true;

            ViewBag.Type = items;
            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        public ActionResult CreateNewQuestionFromThis(int id)
        {
            Question question = db.Questions.Find(id);

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Yes/No", Value = "Yes/No" });

            if (question.Type == "Yes/No")
                items.ElementAt(0).Selected = true;

            ViewBag.Type = items;



            return View(question);
        }

        //
        // POST: /Question/Edit/5

        [HttpPost]
        public ActionResult CreateNewQuestionFromThis(Question question)
        {
            Question newQuestion = new Question { Title = question.Title, Type = question.Type, DateModified = System.DateTime.Now, CompliantAnswer = question.CompliantAnswer, Surveys = new List<Survey>() };
            if (ModelState.IsValid)
            {
                db.Questions.Add(newQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        //
        // GET: /Question/Delete/5
 
        public ActionResult Delete(int id)
        {
            Question question = db.Questions.Find(id);
            return View(question);
        }

        //
        // POST: /Question/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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