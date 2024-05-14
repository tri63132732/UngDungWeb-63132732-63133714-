using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ogani.Models;

namespace Ogani.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        private novelEntities db = new novelEntities();

        // GET: Admin/Comments
        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.chapter).Include(c => c.novel).Include(c => c.user);
            return View(comments.ToList());
        }

        // GET: Admin/Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Admin/Comments/Create
        public ActionResult Create()
        {
            ViewBag.chapter_id = new SelectList(db.chapters, "chapter_id", "title");
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title");
            ViewBag.user_id = new SelectList(db.users, "user_id", "username");
            return View();
        }

        // POST: Admin/Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "comment_id,novel_id,chapter_id,user_id,content,created_at")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.chapter_id = new SelectList(db.chapters, "chapter_id", "title", comment.chapter_id);
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", comment.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", comment.user_id);
            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.chapter_id = new SelectList(db.chapters, "chapter_id", "title", comment.chapter_id);
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", comment.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", comment.user_id);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "comment_id,novel_id,chapter_id,user_id,content,created_at")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.chapter_id = new SelectList(db.chapters, "chapter_id", "title", comment.chapter_id);
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", comment.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", comment.user_id);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
