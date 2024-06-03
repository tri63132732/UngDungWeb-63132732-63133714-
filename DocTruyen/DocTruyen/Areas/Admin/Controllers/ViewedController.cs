using System;
using System.Collections.Generic;
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
    public class ViewedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Viewed

        public ActionResult Index(int? page)
        {
            IEnumerable<Viewed> items = db.Vieweds.OrderBy(x => x.Id);
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
        // GET: Admin/Viewed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewed viewed = db.Vieweds.Include(v => v.Story).Include(v => v.User).FirstOrDefault(v => v.Id == id);
            if (viewed == null)
            {
                return HttpNotFound();
            }
            return View(viewed);
        }

        // GET: Admin/Viewed/Create
        public ActionResult Create()
        {
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            return View();
        }

        // POST: Admin/Viewed/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StoryId,UserId,Comment,CreateDate,UpdateDate")] Viewed viewed)
        {
            if (ModelState.IsValid)
            {
                viewed.CreateDate = DateTime.Now;
                viewed.UpdateDate = DateTime.Now;
                db.Vieweds.Add(viewed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", viewed.StoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", viewed.UserId);
            return View(viewed);
        }

        // GET: Admin/Viewed/Edit/5
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
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", viewed.UserId);
            return View(viewed);
        }

        // POST: Admin/Viewed/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Viewed viewed)
        {
            if (ModelState.IsValid)
            {
                db.Vieweds.Attach(viewed);
                viewed.UpdateDate = DateTime.Now;
                db.Entry(viewed).Property(x => x.StoryId).IsModified = true;
                db.Entry(viewed).Property(x => x.UserId).IsModified = true;
                db.Entry(viewed).Property(x => x.Comment).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", viewed.StoryId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", viewed.UserId);
            return View(viewed);
        }

        // GET: Admin/Viewed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Viewed viewed = db.Vieweds.Include(v => v.Story).Include(v => v.User).FirstOrDefault(v => v.Id == id);
            if (viewed == null)
            {
                return HttpNotFound();
            }
            return View(viewed);
        }

        // POST: Admin/Viewed/Delete/5
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
