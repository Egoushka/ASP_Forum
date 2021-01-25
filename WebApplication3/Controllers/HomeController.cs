using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                if (_context.Themes.Count() == 0)
                {
                    _context.Themes.Add(new Theme()
                    {
                        Title = "Билды",
                        Description = "Описание билдов",
                        IdAuthor = user.Id
                    });

                    _context.Themes.Add(new Theme()
                    {
                        Title = "Беседка",
                        Description = "Истории",
                        IdAuthor = user.Id
                    });

                    _context.Themes.Add(new Theme()
                    {
                        Title = "Гайды",
                        Description = "Различные гайды",
                        IdAuthor = user.Id
                    });
                    _context.SaveChanges();
                }
                if (_context.Posts.Count() == 0)
                {
                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 1,
                        Name = "Билд#1",
                        Text = "Описание билда"
                    });
                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 1,
                        Name = "Билд#2",
                        Text = "Описание билда"
                    });

                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 2,
                        Name = "История#1",
                        Text = "Давным давно..."
                    });
                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 2,
                        Name = "История#2",
                        Text = "Когда-то..."
                    });

                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 3,
                        Name = "Гайд#1",
                        Text = "Для начала..."
                    });
                    _context.Posts.Add(new Post()
                    {
                        IdTheme = 3,
                        Name = "Гайд#2",
                        Text = "Для начала..."
                    });
                    _context.SaveChanges();
                }
                ViewBag.User =
         _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
                ViewBag.Themes = _context.Themes.ToList();
                ViewBag.Posts = _context.Posts.ToList();
            }
      
            return View();
        }
        public IActionResult UserArea()
        {
            ViewBag.Themes = _context.Themes.ToList();
            ViewBag.Posts = _context.Posts.ToList();
            ViewBag.User =
                  _context.Users.FirstOrDefault(u => u.Email == User.Identity.Name);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
