using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectAlumni.Models;

namespace ProjectAlumni.Controllers
{
    [Authorize]
    [HandleError]
    public class VacanciesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Vacancies
        public ActionResult Index()
        {
            var vacancies = db.vacancies.Include(v => v.AspNetUser);
            return View(vacancies.ToList());
        }

        // GET: Vacancies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacancy vacancy = db.vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            return View(vacancy);
        }

        // GET: Vacancies/Create
        public ActionResult Create()
        {
            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Vacancies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vacancyid,title,text,users_userid,date")] vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name.ToString();
                var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
                AspNetUser user = userid[0];

                vacancy.users_userid = user.Id;
                vacancy.date = DateTime.Now;

                db.vacancies.Add(vacancy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", vacancy.users_userid);
            return View(vacancy);
        }

        // GET: Vacancies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacancy vacancy = db.vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", vacancy.users_userid);
            return View(vacancy);
        }

        // POST: Vacancies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vacancyid,title,text,users_userid,date")] vacancy vacancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vacancy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", vacancy.users_userid);
            return View(vacancy);
        }

        // GET: Vacancies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vacancy vacancy = db.vacancies.Find(id);
            if (vacancy == null)
            {
                return HttpNotFound();
            }
            return View(vacancy);
        }

        // POST: Vacancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vacancy vacancy = db.vacancies.Find(id);
            db.vacancies.Remove(vacancy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
