using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.EF
{
    [Table("tb_Story")]
    public class Story: Common
    {
        public Story()
        {
            this.StoryAuthors = new HashSet<StoryAuthor>();
            this.StoryCategories = new HashSet<StoryCategory>();
            this.Chapters = new HashSet<Chapter>();
            this.Vieweds = new HashSet<Viewed>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên truyện không để trống")]
        [StringLength(150)]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Content { get; set; }
        public string source { get; set; }
        public string image { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Viewed> Vieweds { get; set; }
        public virtual ICollection<StoryAuthor> StoryAuthors { get; set; }
        public virtual ICollection<StoryCategory> StoryCategories { get; set; }
    }
}