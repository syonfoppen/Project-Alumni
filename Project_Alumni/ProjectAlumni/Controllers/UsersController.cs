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
    public class UsersController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Users
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.address).Include(a => a.gender);
            return View(aspNetUsers.ToList());
        }
        

        // GET: Users/Edit/5
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
            ViewBag.AddressId = new SelectList(db.addresses, "addressid", "country", aspNetUser.AdressId);
            ViewBag.genderid = new SelectList(db.genders, "genderid", "NAME", aspNetUser.GenderId);
            return View(aspNetUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Description,DateOfBirth,ProfilePicture,AddressId,genderid,hasBeenAccepted")] AspNetUser aspNetUser, HttpPostedFileBase upload)
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
                if (aspNetUser.EmailConfirmed != false)
                {
                    currentUser.EmailConfirmed = aspNetUser.EmailConfirmed;
                }
                if (aspNetUser.PhoneNumber != String.Empty && aspNetUser.PhoneNumber != "" && aspNetUser.PhoneNumber != null)
                {
                    currentUser.PhoneNumber = aspNetUser.PhoneNumber;
                }
                if (aspNetUser.PhoneNumber != String.Empty && aspNetUser.PhoneNumber != "" && aspNetUser.PhoneNumber != null)
                {
                    currentUser.PhoneNumber = aspNetUser.PhoneNumber;
                }
                if (aspNetUser.LockoutEnabled != false)
                {
                    currentUser.LockoutEnabled = aspNetUser.LockoutEnabled;
                }
                if (aspNetUser.Lastname != String.Empty && aspNetUser.Lastname != "" && aspNetUser.Lastname != null)
                {
                    currentUser.Lastname = aspNetUser.Lastname;
                }
                if (aspNetUser.UserName != String.Empty && aspNetUser.UserName != "" && aspNetUser.UserName != null)
                {
                    currentUser.UserName = aspNetUser.UserName;
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
                if (aspNetUser.HasBeenAccepted != false)
                {
                    currentUser.HasBeenAccepted = aspNetUser.HasBeenAccepted;
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

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
