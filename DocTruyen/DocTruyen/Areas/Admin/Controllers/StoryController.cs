using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocTruyen.Models;
using DocTruyen.Models.EF;
using PagedList;

namespace DocTruyen.Areas.Admin.Controllers
{
    public class StoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Story
        public ActionResult Index(int? page)
        {
            IEnumerable<Story> items = db.Stories.OrderBy(x => x.Name);
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        // GET: Admin/Story/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Admin/Story/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Story/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Alias,Content,source,image,Description,Keyword,CreateDate,UpdateDate")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.CreateDate = DateTime.Now;
                story.UpdateDate = DateTime.Now;
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(story);
        }

        // GET: Admin/Story/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Admin/Story/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Attach(story);
                story.UpdateDate = DateTime.Now;
                db.Entry(story).Property(x => x.Name).IsModified = true;
                db.Entry(story).Property(x => x.Alias).IsModified = true;
                db.Entry(story).Property(x => x.Content).IsModified = true;
                db.Entry(story).Property(x => x.source).IsModified = true;
                db.Entry(story).Property(x => x.image).IsModified = true;
                db.Entry(story).Property(x => x.Description).IsModified = true;
                db.Entry(story).Property(x => x.Keyword).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        // GET: Admin/Story/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Admin/Story/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
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
