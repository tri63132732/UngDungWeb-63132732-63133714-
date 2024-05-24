using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.EF
{
    [Table("tb_Author")]
    public class Author: Common
    {
        public Author() {
            this.StoryAuthors = new HashSet<StoryAuthor>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên tác giả không để trống")]
        [StringLength(150)]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public virtual ICollection<StoryAuthor> StoryAuthors { get; set; }
    }
}