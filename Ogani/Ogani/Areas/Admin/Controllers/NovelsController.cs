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
    public class NovelsController : Controller
    {
        private novelEntities db = new novelEntities();

        // GET: Admin/Novels
        public ActionResult Index()
        {
            return View(db.novels.ToList());
        }

        // GET: Admin/Novels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            novel novel = db.novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            return View(novel);
        }

        // GET: Admin/Novels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Novels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "novel_id,title,author,description,cover_image_url,created_at,updated_at")] novel novel)
        {
            if (ModelState.IsValid)
            {
                db.novels.Add(novel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(novel);
        }

        // GET: Admin/Novels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            novel novel = db.novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            return View(novel);
        }

        // POST: Admin/Novels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "novel_id,title,author,description,cover_image_url,created_at,updated_at")] novel novel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(novel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(novel);
        }

        // GET: Admin/Novels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            novel novel = db.novels.Find(id);
            if (novel == null)
            {
                return HttpNotFound();
            }
            return View(novel);
        }

        // POST: Admin/Novels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            novel novel = db.novels.Find(id);
            db.novels.Remove(novel);
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
