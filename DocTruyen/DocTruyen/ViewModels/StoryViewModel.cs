using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocTruyen.ViewModels
{
    public class StoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ChapterViewModel> Chapters { get; set; }
    }
}