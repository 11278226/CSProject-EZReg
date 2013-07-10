using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EZ_Regulatory2.Models;
using EZ_Regulatory2.DAL;
using System.Data.Entity.Infrastructure;

namespace EZ_Regulatory2.Controllers
{
    public class SurveysController : Controller
    {
        private SurveyContext db = new SurveyContext();

        //
        // GET: /Surveys/

        public ActionResult Index()
        {
            ViewBag.Message = "Create and Distribute Surveys here.";
            return View(db.Surveys.ToList());
        }

        //
        // GET: /Surveys/Details/5

        public ActionResult Details(int id = 0)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        //
        // GET: /Surveys/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Surveys/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // GET: /Surveys/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        //
        // POST: /Surveys/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        // GET: /Surveys/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        //
        // POST: /Surveys/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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