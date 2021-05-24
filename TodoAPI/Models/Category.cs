using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class Category
    {
        [Key,Required]
        public int CategoryID { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
