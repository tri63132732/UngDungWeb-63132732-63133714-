using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocTruyen.Models
{
    public abstract class Common
    {
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}