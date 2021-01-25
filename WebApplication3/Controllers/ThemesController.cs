using Microsoft.AspNetCore.Identity;
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
    public class ThemesController : Controller
    {
        private ApplicationContext _context;

        public ThemesController(ApplicationContext context)
        {

            _context = context;
        }

        public IActionResult Index() {
            ViewBag.User =
                _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name); 
            ViewBag.Themes = _context.Themes.ToList();
            ViewBag.Posts = _context.Posts.ToList();
            return View(_context.Themes.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateThemeViewModel model)
        {

            Theme theme = new Theme { Description = model.Description, Title = model.Title, IdAuthor = User.FindFirstValue(ClaimTypes.NameIdentifier) };
            _context.Themes.Add(theme);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            bool isFind = false;
            Theme theme = new Theme() ;
            EditThemeViewModel model = new EditThemeViewModel();
            int idInteger = int.Parse(id);
            foreach(var item in _context.Themes)
            {
                if (item.Id == idInteger)
                {
                    model = new EditThemeViewModel { Id = item.Id, Title = item.Title, Description = item.Description };
                    theme = item;
                    isFind = true;
                    break;
                }

            }
            if (isFind)
            {
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditThemeViewModel model)
        {
   
             Theme theme = new Theme() { Id = model.Id, Description = model.Description, IdAuthor = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name).Id, Title = model.Title };
            _context.Themes.Update(theme);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            bool isFind = false;
            Theme theme = new Theme();
            
            int idInteger = int.Parse(id);
            foreach (var item in _context.Themes)
            {
                if (item.Id == idInteger)
                {
                    theme = item;
                    isFind = true;
                    break;
                }

            }
            if (isFind)
            {
                var list = _context.Posts.Where(p => p.IdTheme == theme.Id);
                _context.Posts.RemoveRange(list); _context.Themes.Remove(theme);            
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
