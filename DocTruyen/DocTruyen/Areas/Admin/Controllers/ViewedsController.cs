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
    public class ViewedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Vieweds
        public ActionResult Index(int? page)
        {
            // Fetch the data including related Story and User entities
            var items = db.Vieweds
                          .Include(v => v.Story)
                          .Include(v => v.User)
                          .OrderBy(x => x.CreateDate);

            // Set page size
            var pageSize = 5;
            var pageIndex = page ?? 1;

            // Convert to paged list
            var pagedItems = items.ToPagedList(pageIndex, pageSize);

            // Set ViewBag properties for pagination
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;

            // Return the view with paged items
            return View(pagedItems);
        }

        // GET: Admin/Vieweds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewed viewed = db.Vieweds.Find(id);
            if (viewed == null)
            {
                return HttpNotFound();
            }
            return View(viewed);
        }

        // GET: Admin/Vieweds/Create
        public ActionResult Create()
        {
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name");
            return View();
        }

        // POST: Admin/Vieweds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoryId,UserId,Comment,CreateDate,UpdateDate")] Viewed viewed)
        {
            if (ModelState.IsValid)
            {
                db.Vieweds.Add(viewed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", viewed.StoryId);
            return View(viewed);
        }

        // GET: Admin/Vieweds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewed viewed = db.Vieweds.Find(id);
            if (viewed == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", viewed.StoryId);
            return View(viewed);
        }

        // POST: Admin/Vieweds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StoryId,UserId,Comment,CreateDate,UpdateDate")] Viewed viewed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", viewed.StoryId);
            return View(viewed);
        }

        // GET: Admin/Vieweds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewed viewed = db.Vieweds.Find(id);
            if (viewed == null)
            {
                return HttpNotFound();
            }
            return View(viewed);
        }

        // POST: Admin/Vieweds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Viewed viewed = db.Vieweds.Find(id);
            db.Vieweds.Remove(viewed);
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
