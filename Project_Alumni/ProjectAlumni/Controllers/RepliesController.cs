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
    [HandleError]
    [Authorize]
    public class RepliesController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Replies/Create
        public ActionResult Create()
        {
            ViewBag.posts_postid = new SelectList(db.posts, "postid", "title");
            return View();
        }

        // POST: Replies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "repliesid,text")] reply reply, string postID)
        {
            if (ModelState.IsValid)
            {
                reply.date = DateTime.Now;
                reply.username = User.Identity.Name;
                reply.posts_postid = Convert.ToInt32(postID);
                db.replies.Add(reply);
                db.SaveChanges();
                return RedirectToRoute(new
                {
                    controller = "Post",
                    action = "Details",
                    id = Convert.ToInt32(postID)
                });
            }

            ViewBag.posts_postid = new SelectList(db.posts, "postid", "title", reply.posts_postid);
            
            return View(reply);
        }

        // GET: Replies/Edit/5
        public ActionResult Edit(int? id , int? postid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reply reply = db.replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            ViewBag.posts_postid = new SelectList(db.posts, "postid", "title", reply.posts_postid);
            return View(reply);
        }

        // POST: Replies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "repliesid,text,date,posts_postid")] reply reply, int postid)
        {
            if (ModelState.IsValid)
            {
                reply.date = DateTime.Now;
                reply.posts_postid = postid;
                reply.username = User.Identity.Name;
                db.Entry(reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToRoute(new
                {
                    controller = "Post",
                    action = "Details",
                    id = postid
                });
            }
            ViewBag.posts_postid = new SelectList(db.posts, "postid", "title", reply.posts_postid);
            return View(reply);
        }

        // GET: Replies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            reply reply = db.replies.Find(id);
            if (reply == null)
            {
                return HttpNotFound();
            }
            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reply reply = db.replies.Find(id);
            db.replies.Remove(reply);
            db.SaveChanges();
            return RedirectToAction("Index", "Post");
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
