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
    public class ManagerChecklistController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /ManagerChecklist/

        public ViewResult Index()
        {
            List<User> users = db.Users
            .Where(i => i.SurveyAnswers.Count() != 0)
                .ToList();
            //List<Survey> surveys = db.Surveys.ToList();
            //List<Survey> filteredList = new List<Survey>();

            //foreach (Survey s in surveys)
            //{
            //    foreach (User u in users) 
            //    {
            //        foreach (SurveyAnswer sa in u.SurveyAnswers)
            //        {
            //            if (s.ID == sa.SurveyID)
            //            {
            //                filteredList.Add(s);
            //           }
            //        }
            //    }
            //    
            //}

            return View(users);
        }

        //
        // GET: /ManagerChecklist/Details/5

        public ViewResult Details(int id, int surveyid)
        {
            SurveyAnswer surveyanswer = db.SurveyAnswers.ToList()
                .Where(i => i.UserID == id && i.SurveyID == surveyid)
                .Single();

            User user = db.Users.Find(id);
            ViewBag.UserName = user.Name;
            Survey survey = db.Surveys.Find(surveyid);
            ViewBag.Questions = survey.Questions.ToList();
            
            return View(surveyanswer);
        }

        //
        // GET: /ManagerChecklist/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ManagerChecklist/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /ManagerChecklist/Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /ManagerChecklist/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /ManagerChecklist/Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /ManagerChecklist/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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