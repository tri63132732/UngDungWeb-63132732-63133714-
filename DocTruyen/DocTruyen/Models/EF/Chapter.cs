using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocTruyen.Models.EF
{
    [Table("tb_Chapter")]
    public class Chapter: Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên chương không để trống")]
        [StringLength(150)]
        public string Name { get; set; }
        public string Alias { get; set; }
        [AllowHtml] public string Content { get; set; }
        
        public string Keyword { get; set; }
        public int StoryId { get; set; }
        public virtual Story Story { get; set; }
    }
}