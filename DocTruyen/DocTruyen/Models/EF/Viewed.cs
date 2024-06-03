using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.EF
{
    [Table("tb_Viewed")]
    public class Viewed: Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StoryId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public virtual Story Story { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}