using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int IdTheme { get; set; }
        public string IdAuthor { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        
    }
}
