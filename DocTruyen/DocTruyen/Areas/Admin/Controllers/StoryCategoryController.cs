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
    [Authorize(Roles = "Admin, User")]
    public class StoryCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/StoryCategory
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            var pageIndex = page ?? 1;

            // Fetch stories along with their authors
            var storiesCategories = db.StoriesCategories.Include(s => s.Category).Include(s => s.Story)
                                                .OrderBy(s => s.Story.Name);

            // Paginate the results
            var paginatedStories = storiesCategories.ToPagedList(pageIndex, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageIndex;

            return View(paginatedStories);
        }
        // GET: Admin/StoryCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryCategory storyCategory = db.StoriesCategories.Find(id);
            if (storyCategory == null)
            {
                return HttpNotFound();
            }
            return View(storyCategory);
        }

        // GET: Admin/StoryCategory/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name");
            return View();
        }

        // POST: Admin/StoryCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoryId,CategoryId,CreateDate,UpdateDate")] StoryCategory storyCategory)
        {
            if (ModelState.IsValid)
            {
                storyCategory.CreateDate = DateTime.Now;
                storyCategory.UpdateDate = DateTime.Now;
                db.StoriesCategories.Add(storyCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", storyCategory.CategoryId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyCategory.StoryId);
            return View(storyCategory);
        }

        // GET: Admin/StoryCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryCategory storyCategory = db.StoriesCategories.Find(id);
            if (storyCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", storyCategory.CategoryId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyCategory.StoryId);
            return View(storyCategory);
        }

        // POST: Admin/StoryCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoryId,CategoryId,CreateDate,UpdateDate")] StoryCategory storyCategory)
        {
            if (ModelState.IsValid)
            {
                storyCategory.UpdateDate = DateTime.Now;
                db.Entry(storyCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", storyCategory.CategoryId);
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", storyCategory.StoryId);
            return View(storyCategory);
        }

        // GET: Admin/StoryCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StoryCategory storyCategory = db.StoriesCategories.Find(id);
            if (storyCategory == null)
            {
                return HttpNotFound();
            }
            return View(storyCategory);
        }

        // POST: Admin/StoryCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StoryCategory storyCategory = db.StoriesCategories.Find(id);
            db.StoriesCategories.Remove(storyCategory);
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
