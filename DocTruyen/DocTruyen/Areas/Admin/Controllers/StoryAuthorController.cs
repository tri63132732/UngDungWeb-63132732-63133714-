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
    public class StoryAuthorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/StoryAuthor
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            var pageIndex = page ?? 1;

            // Fetch stories along with their authors
            var storiesAuthor = db.StoriesAuthor.Include(s => s.Author).Include(s => s.Story)
                                                .OrderBy(s => s.Story.Name);

            // Paginate the results
            var paginatedStories = storiesAuthor.ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;

            return View(paginatedStories);
        }
        // GET: Admin/StoryAuthor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryAuthor storyAuthor = db.StoriesAuthor.Find(id);
            if (storyAuthor == null)
            {
                return HttpNotFound();
            }
            return View(storyAuthor);
        }

        // GET: Admin/StoryAuthor/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authorities, "Id", "Name");
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name");
            return View();
        }

        // POST: Admin/StoryAuthor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoryId,AuthorId,CreateDate,UpdateDate")] StoryAuthor storyAuthor)
        {
            if (ModelState.IsValid)
            {
                storyAuthor.CreateDate = DateTime.Now;
                storyAuthor.UpdateDate = DateTime.Now;
                db.StoriesAuthor.Add(storyAuthor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authorities, "Id", "Name", storyAuthor.AuthorId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyAuthor.StoryId);
            return View(storyAuthor);
        }

        // GET: Admin/StoryAuthor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryAuthor storyAuthor = db.StoriesAuthor.Find(id);
            if (storyAuthor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authorities, "Id", "Name", storyAuthor.AuthorId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyAuthor.StoryId);
            return View(storyAuthor);
        }

        // POST: Admin/StoryAuthor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoryId,AuthorId,CreateDate,UpdateDate")] StoryAuthor storyAuthor)
        {
            if (ModelState.IsValid)
            {
                storyAuthor.UpdateDate = DateTime.Now;
                db.Entry(storyAuthor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authorities, "Id", "Name", storyAuthor.AuthorId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyAuthor.StoryId);
            return View(storyAuthor);
        }

        // GET: Admin/StoryAuthor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryAuthor storyAuthor = db.StoriesAuthor.Find(id);
            if (storyAuthor == null)
            {
                return HttpNotFound();
            }
            return View(storyAuthor);
        }

        // POST: Admin/StoryAuthor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoryAuthor storyAuthor = db.StoriesAuthor.Find(id);
            db.StoriesAuthor.Remove(storyAuthor);
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
