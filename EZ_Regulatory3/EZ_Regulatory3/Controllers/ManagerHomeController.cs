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
    public class ManagerHomeController : Controller
    {
        private SurveyDBContext db = new SurveyDBContext();

        //
        // GET: /ManagerHome/

        public ViewResult Index()
        {
            User thisUser = db.Users.Find(1);
            ViewBag.User = thisUser;
            return View(db.Users.ToList());
        }

        //
        // GET: /ManagerHome/Details/5

        public ViewResult Details(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // GET: /ManagerHome/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ManagerHome/Create

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
        // GET: /ManagerHome/Edit/5

        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /ManagerHome/Edit/5

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
        // GET: /ManagerHome/Delete/5

        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }

        //
        // POST: /ManagerHome/Delete/5

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