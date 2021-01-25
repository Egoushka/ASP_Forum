using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationContext _context;
        private static int _IdTheme = -1;
        public PostsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index(string id)
        {
            if (!string.Equals(id, null))
            {
                _IdTheme = int.Parse(id);
            }
            ViewBag.Posts = _context.Posts.Where(p => p.IdTheme.Equals(_IdTheme)).ToList();          
            return View();
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {

            Post post = new Post { IdTheme = _IdTheme, Name = model.Name, Text = model.Text };
            _context.Posts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            foreach (var item in _context.Posts)
            {
                if (item.Id == _IdTheme)
                {
                    return View(new EditPostViewModel { Id = item.Id, IdAuthor= item.IdAuthor, Name = item.Name, Text = item.Text });
                }

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostViewModel model)
        {

            Post post = new Post() { Id = model.Id, IdAuthor = model.IdAuthor,Name = model.Name, Text = model.Text, IdTheme = _IdTheme};
            _context.Posts.Update(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            bool isFind = false;
            Post post = new Post();

            int idInteger = int.Parse(id);
            foreach (var item in _context.Posts)
            {
                if (item.Id == idInteger)
                {
                    post = item;
                    isFind = true;
                    break;
                }

            }
            if (isFind)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
