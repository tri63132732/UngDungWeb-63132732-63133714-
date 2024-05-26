using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace DocTruyen.Models.EF
{
    [Table("tb_Category")]
    public class Category : Common
    {
        public Category()
        {
            this.StoryCategories = new HashSet<StoryCategory>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên chuyên mục không để trống")]
        [StringLength(150)]
        public string Name { get; set; }
        public string Keyword { get; set; }
        public virtual ICollection<StoryCategory> StoryCategories { get; set; }
    }
}