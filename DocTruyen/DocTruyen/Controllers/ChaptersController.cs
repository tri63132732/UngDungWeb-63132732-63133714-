using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DocTruyen.Models;
using DocTruyen.Models.EF;

namespace DocTruyen.Controllers
{
    public class ChaptersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Chapters
        public ActionResult Index()
        {
            var chapters = db.Chapters.Include(c => c.Story);
            return View(chapters.ToList());
        }

        // GET: Chapters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            var content = HttpUtility.HtmlDecode(chapter.Content);

            // Remove HTML tags but keep line breaks
            content = Regex.Replace(content, "<br\\s*/?>", "\n"); // Replace <br> with newline
            content = Regex.Replace(content, "<.*?>", String.Empty); // Remove other HTML tags

            // Pass the processed content along with the story to the view
            ViewBag.Content = content;

            var vieweds = db.Vieweds.Include(v => v.Story).Include(v => v.User)
                            .Where(v => v.StoryId == chapter.StoryId).ToList();
            ViewBag.Vieweds = vieweds;
            return View(chapter);
        }

        // GET: Chapters/Create
        public ActionResult Create()
        {
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name");
            return View();
        }

        // POST: Chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Alias,Content,Keyword,StoryId,CreateDate,UpdateDate")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Chapters.Add(chapter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", chapter.StoryId);
            return View(chapter);
        }

        // GET: Chapters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", chapter.StoryId);
            return View(chapter);
        }

        // POST: Chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Alias,Content,Keyword,StoryId,CreateDate,UpdateDate")] Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chapter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoryId = new SelectList(db.Stories, "Id", "Name", chapter.StoryId);
            return View(chapter);
        }

        // GET: Chapters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chapter chapter = db.Chapters.Find(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return View(chapter);
        }

        // POST: Chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chapter chapter = db.Chapters.Find(id);
            db.Chapters.Remove(chapter);
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



        public async Task<ActionResult> GetNextChapter(int currentChapterId)
        {
            var currentChapter = await db.Chapters.FindAsync(currentChapterId);
            if (currentChapter == null)
            {
                return HttpNotFound();
            }

            var storyId = currentChapter.StoryId;
            var story = await db.Stories
                .Include(s => s.Chapters)
                .FirstOrDefaultAsync(s => s.Id == storyId);

            var nextChapter = story.GetNextChapter(currentChapterId);
            if (nextChapter == null)
            {
                return HttpNotFound();
            }

            // Truyền ID của chapter tiếp theo trực tiếp vào action Details trong URL
            var nextChapterId = nextChapter.Id;
            return RedirectToAction("Details", "Chapters", new { id = nextChapterId });
        }

        public async Task<int> GetTotalNumberOfChapters(int storyId)
        {
            var totalNumberOfChapters = await db.Chapters
                .Where(c => c.StoryId == storyId)
                .CountAsync();

            return totalNumberOfChapters;
        }

        public async Task<ActionResult> GetPreviousChapter(int currentChapterId)
        {
            var currentChapter = await db.Chapters.FindAsync(currentChapterId);
            if (currentChapter == null)
            {
                return HttpNotFound();
            }

            var storyId = currentChapter.StoryId;

            // Tính tổng số lượng chapter của câu chuyện
            var totalNumberOfChapters = await GetTotalNumberOfChapters(storyId);

            var story = await db.Stories
                .Include(s => s.Chapters)
                .FirstOrDefaultAsync(s => s.Id == storyId);

            var previousChapter = story.GetPreviousChapter(currentChapterId);
            if (previousChapter == null)
            {
                return HttpNotFound();
            }

            // Truyền ID của chapter trước đó trực tiếp vào action Details trong URL
            var previousChapterId = previousChapter.Id;

            // Truyền giá trị TotalNumberOfChapters qua ViewBag
            ViewBag.TotalNumberOfChapters = totalNumberOfChapters;

            return RedirectToAction("Details", "Chapters", new { id = previousChapterId });
        }


    }
}
