using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using DocTruyen.Models;
using DocTruyen.Models.EF;
using PagedList;

namespace DocTruyen.Controllers
{
    public class StoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stories
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

        // GET: Stories/Details/5
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

            StoryAuthor storyAuthor = db.StoriesAuthor.FirstOrDefault(sa => sa.StoryId == id);
            if (storyAuthor == null)
            {
                // Handle the case where the author is not found
                // You can return HttpNotFound() or any other appropriate action
                return HttpNotFound();
            }
            Author author = db.Authorities.Find(storyAuthor.AuthorId);
            ViewBag.Author = author;

            // Retrieve the categories associated with this story
            var categories = db.StoriesCategories
                                .Where(sc => sc.StoryId == id)
                                .Select(sc => sc.Category)
                                .ToList();

            // Pass both the story and categories to the view
            ViewBag.Categories = categories;

            // Retrieve all chapters associated with this story
            var chapters = db.Chapters.Where(c => c.StoryId == id).ToList();

            // Pass the chapters along with the story to the view
            ViewBag.Chapters = chapters;

            // Decode HTML entities
            var content = HttpUtility.HtmlDecode(story.Content);

            // Remove HTML tags
            content = Regex.Replace(content, "<.*?>", String.Empty);

            // Replace HTML line breaks with newline characters
            content = content.Replace("<br>", "\n");

            // Add spaces after punctuation marks to preserve line breaks
            content = content.Replace(".", ". ");
            content = content.Replace("?", "? ");
            content = content.Replace("!", "! ");
            // Add more punctuation marks as needed

            // Pass the processed content along with the story to the view
            ViewBag.Content = content;

            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Alias,Content,source,image,Description,Keyword,CreateDate,UpdateDate")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(story);
        }

        // GET: Stories/Edit/5
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

        // POST: Stories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Alias,Content,source,image,Description,Keyword,CreateDate,UpdateDate")] Story story)
        {
            if (ModelState.IsValid)
            {
                db.Entry(story).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(story);
        }

        // GET: Stories/Delete/5
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

        // POST: Stories/Delete/5
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
