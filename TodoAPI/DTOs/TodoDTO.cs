using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.DTOs
{
    public class TodoDTO
    {
        public int todoID { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
        public int categoryID { get; set; }
        public string categoryName { get; set; }
    }
}
