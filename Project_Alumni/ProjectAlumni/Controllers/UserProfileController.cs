using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectAlumni.Models;

namespace ProjectAlumni.Controllers
{
    [Authorize]
    [HandleError]
    public class UserProfileController : Controller
    {
        CultureInfo culture = new CultureInfo("nl-NL");
        private DatabaseEntities db = new DatabaseEntities();
        
        
        // GET: UserProfile/Edit/5
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];
            if (user.Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user.DateOfBirth = Convert.ToDateTime(user.DateOfBirth, culture);
            ViewBag.date = user.DateOfBirth;
            ViewBag.GenderId = new SelectList(db.genders, "genderid", "NAME", user.GenderId);
            return View(user);
        }

        // POST: UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,ProfilePicture,AdressId,GenderId,Firstname,Lastname,DateOfBirth,Description,GraduationYear,ProfilePicture")] AspNetUser aspNetUser, HttpPostedFileBase upload)
        {
            AspNetUser currentUser = new AspNetUser();
            if (ModelState.IsValid)
            {
                byte[] profilePicture = null;
                if (upload != null && upload.ContentLength > 0)
                {

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                         profilePicture = reader.ReadBytes(upload.ContentLength);
                    }
                }

                currentUser = db.AspNetUsers.FirstOrDefault(p => p.Id == aspNetUser.Id);
                if (currentUser == null)
                    return HttpNotFound();

                if (profilePicture != null)
                {
                    currentUser.ProfilePicture = profilePicture;
                }
                if (aspNetUser.Email != String.Empty && aspNetUser.Email != "" && aspNetUser.Email != null)
                {
                    currentUser.Email = aspNetUser.Email;
                }
                if (aspNetUser.PhoneNumber != String.Empty && aspNetUser.PhoneNumber != "" && aspNetUser.PhoneNumber != null)
                {
                    currentUser.PhoneNumber = aspNetUser.PhoneNumber;
                }
                if (aspNetUser.Firstname != String.Empty && aspNetUser.Firstname != "" && aspNetUser.Firstname != null)
                {
                    currentUser.Firstname = aspNetUser.Firstname;
                }
                if (aspNetUser.Lastname != String.Empty && aspNetUser.Lastname != "" && aspNetUser.Lastname != null)
                {
                    currentUser.Lastname = aspNetUser.Lastname;
                }
                if (aspNetUser.DateOfBirth != null)
                {
                    currentUser.DateOfBirth = aspNetUser.DateOfBirth;
                }
                if (aspNetUser.Description != String.Empty && aspNetUser.Description != "" && aspNetUser.Description != null)
                {
                    currentUser.Description = aspNetUser.Description;
                }
                if (aspNetUser.GraduationYear != 0 && aspNetUser.GraduationYear != null)
                {
                    currentUser.GraduationYear = aspNetUser.GraduationYear;
                }
                if (aspNetUser.GenderId != 0 && aspNetUser.GenderId != null)
                {
                    currentUser.GenderId = aspNetUser.GenderId;
                }

                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
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
            return View(aspNetUser);
        }
    }
}
