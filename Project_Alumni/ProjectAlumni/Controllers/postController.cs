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
    public class PostController : Controller
    {
        private DatabaseEntities db = new DatabaseEntities();

        // GET: Post
        public ActionResult Index()
        {
            //Get the user id of the current user and put it in a viewbag
            string username = User.Identity.Name.ToString();
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];
            ViewBag.userid = user.Id;


            var post = db.posts.OrderByDescending(x => x.postid).ToList();
            return View(post);
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            var replies = db.replies.SqlQuery("SELECT * FROM replies WHERE posts_postid = " + "'" + post.postid + "'").OrderByDescending(x => x.repliesid).ToList();
            ViewBag.postid = post.postid;
            ViewBag.replies = replies;
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            
            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postid,title,text,users_userid,date")] post post)
        {
            if (ModelState.IsValid)
            {
                //Get the user id of the current user and add it to the post
                string username = User.Identity.Name.ToString();
                var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
                AspNetUser user = userid[0];
                post.users_userid = user.Id;

                //Get the current data and add it to the post
                post.date = DateTime.Now;

                db.posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", post.users_userid);
            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            //look if the user has permisions to edite the file
            string username = User.Identity.Name.ToString();
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];

            if (post.users_userid == user.Id || User.IsInRole("Admin"))
            {
                ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", post.users_userid);
                return View(post);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postid,title,text,users_userid,date")] post post)
        {
            if (ModelState.IsValid)
            {
                //Fin the user id
                string username = User.Identity.Name.ToString();
                var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
                AspNetUser user = userid[0];
                post.date = DateTime.Now;
                post.users_userid = user.Id;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", post.users_userid);
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            post post = db.posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            

            //look if the user has permisions to edite the file
            string username = User.Identity.Name.ToString();
            var userid = db.AspNetUsers.SqlQuery("SELECT * FROM AspNetUsers WHERE UserName = " + "'" + username + "'").ToList();
            AspNetUser user = userid[0];

            if (post.users_userid == user.Id || User.IsInRole("Admin"))
            {
                ViewBag.users_userid = new SelectList(db.AspNetUsers, "Id", "Email", post.users_userid);
                return View(post);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            post post = db.posts.Find(id);
            db.posts.Remove(post);
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
