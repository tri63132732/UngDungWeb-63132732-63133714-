using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocTruyen.Models.EF
{
    [Table("tb_StoryCategory")]
    public class StoryCategory: Common
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StoryId { get; set; }
        public int CategoryId { get; set; }
        public virtual Story Story { get; set; }
        public virtual Category Category { get; set; }
    }
}