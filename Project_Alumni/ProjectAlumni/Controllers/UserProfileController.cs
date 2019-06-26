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
    public class UserProfileController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: UserProfile
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];
            return RedirectToAction(user.Id);
        }
        
        // GET: UserProfile/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdressId = new SelectList(db.addresses, "addressid", "country", aspNetUser.AdressId);
            ViewBag.GenderId = new SelectList(db.genders, "genderid", "NAME", aspNetUser.GenderId);
            return View(aspNetUser);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ProfilePicture,AdressId,GenderId,HasBeenAccepted,Firstname,Lastname,DateOfBirth,Description,GraduationYear")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdressId = new SelectList(db.addresses, "addressid", "country", aspNetUser.AdressId);
            ViewBag.GenderId = new SelectList(db.genders, "genderid", "NAME", aspNetUser.GenderId);
            return View(aspNetUser);
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
