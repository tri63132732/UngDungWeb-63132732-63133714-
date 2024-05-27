using DocTruyen.Models.EF;
using DocTruyen.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DocTruyen.ViewModels;

namespace DocTruyen.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stories
        public ActionResult Index()
        {
            var stories = db.Stories.OrderByDescending(x => x.UpdateDate).Take(8).ToList();
            var chapters = db.Chapters.OrderByDescending(x => x.UpdateDate).Take(3).ToList();

            var storyViewModels = stories.Select(story => new StoryViewModel
            {
                Id = story.Id,
                Name = story.Name,
                Image = story.image,
                Chapters = chapters
                    .Where(chapter => chapter.StoryId == story.Id)
                    .Select(chapter => new ChapterViewModel
                    {
                        Id = chapter.Id,
                        Name = chapter.Name
                    }).ToList()
            }).ToList();

            return View(storyViewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}