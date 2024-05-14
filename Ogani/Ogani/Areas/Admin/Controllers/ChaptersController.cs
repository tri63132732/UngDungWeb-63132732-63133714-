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
    public class ChaptersController : Controller
    {
        private novelEntities db = new novelEntities();

        // GET: Admin/Chapters
        public ActionResult Index()
        {
            var chapters = db.chapters.Include(c => c.novel);
            return View(chapters.ToList());
        }

        // GET: Admin/Chapters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chapter chapter = db.chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // GET: Admin/Chapters/Create
        public ActionResult Create()
        {
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title");
            return View();
        }

        // POST: Admin/Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "chapter_id,novel_id,chapter_number,title,content,is_available,created_at,updated_at")] chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", chapter.novel_id);
            return View(chapter);
        }

        // GET: Admin/Chapters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chapter chapter = db.chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", chapter.novel_id);
            return View(chapter);
        }

        // POST: Admin/Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "chapter_id,novel_id,chapter_number,title,content,is_available,created_at,updated_at")] chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.novel_id = new SelectList(db.novels, "novel_id", "title", chapter.novel_id);
            return View(chapter);
        }

        // GET: Admin/Chapters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chapter chapter = db.chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Admin/Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chapter chapter = db.chapters.Find(id);
            db.chapters.Remove(chapter);
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
