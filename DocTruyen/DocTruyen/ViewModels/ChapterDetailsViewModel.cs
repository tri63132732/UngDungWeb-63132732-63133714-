using DocTruyen.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.ViewModels
{
    public class ChapterDetailsViewModel
    {
        public Chapter Chapter { get; set; }
        public IEnumerable<Viewed> Vieweds { get; set; }
    }
}