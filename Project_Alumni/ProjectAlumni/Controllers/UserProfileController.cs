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
    public class UserProfileController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: UserProfile
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];
            return RedirectToAction("Edit");
        }
        
        // GET: UserProfile/Edit/5
        public ActionResult Edit()
        {
            var username = User.Identity.Name;
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];
            if (user.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(user.Id);
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
        public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,UserName,ProfilePicture,AdressId,GenderId,Firstname,Lastname,DateOfBirth,Description,GraduationYear,ProfilePicture")] AspNetUser aspNetUser, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        aspNetUser.ProfilePicture = reader.ReadBytes(upload.ContentLength);
                    }
                }
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
