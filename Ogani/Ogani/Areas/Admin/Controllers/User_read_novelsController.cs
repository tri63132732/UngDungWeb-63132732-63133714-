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
    public class User_read_novelsController : Controller
    {
        private novelEntities db = new novelEntities();

        // GET: Admin/User_read_novels
        public ActionResult Index()
        {
            var user_read_novels = db.user_read_novels.Include(u => u.novel).Include(u => u.user);
            return View(user_read_novels.ToList());
        }

        // GET: Admin/User_read_novels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_read_novels user_read_novels = db.user_read_novels.Find(id);
            if (user_read_novels == null)
            {
                return HttpNotFound();
            }
            return View(user_read_novels);
        }

        // GET: Admin/User_read_novels/Create
        public ActionResult Create()
        {
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title");
            ViewBag.user_id = new SelectList(db.users, "user_id", "username");
            return View();
        }

        // POST: Admin/User_read_novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,novel_id,last_read_chapter_id")] user_read_novels user_read_novels)
        {
            if (ModelState.IsValid)
            {
                db.user_read_novels.Add(user_read_novels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", user_read_novels.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", user_read_novels.user_id);
            return View(user_read_novels);
        }

        // GET: Admin/User_read_novels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_read_novels user_read_novels = db.user_read_novels.Find(id);
            if (user_read_novels == null)
            {
                return HttpNotFound();
            }
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", user_read_novels.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", user_read_novels.user_id);
            return View(user_read_novels);
        }

        // POST: Admin/User_read_novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,novel_id,last_read_chapter_id")] user_read_novels user_read_novels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_read_novels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", user_read_novels.novel_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", user_read_novels.user_id);
            return View(user_read_novels);
        }

        // GET: Admin/User_read_novels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_read_novels user_read_novels = db.user_read_novels.Find(id);
            if (user_read_novels == null)
            {
                return HttpNotFound();
            }
            return View(user_read_novels);
        }

        // POST: Admin/User_read_novels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_read_novels user_read_novels = db.user_read_novels.Find(id);
            db.user_read_novels.Remove(user_read_novels);
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
