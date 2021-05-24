using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class Todo
    {
        [Column("id")]
        [Key]
        public int TodoID { get; set; }
        [Column("title")]
        [Required]
        public string Title { get; set; }
        [Column("completed")]
        [Required]
        public bool Completed { get; set; }
        [Column("category_id")]
        [Required]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category category { get; set; }
    }
}
