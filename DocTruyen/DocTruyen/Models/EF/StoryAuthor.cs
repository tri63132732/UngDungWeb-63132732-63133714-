using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.EF
{
    [Table("tb_StoryAuthor")]
    public class StoryAuthor:Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int AuthorId { get; set; }
        public virtual Story Story { get; set; }
        public virtual Author Author { get; set; }
    }
}