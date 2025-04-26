using System.Diagnostics;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly masterDbContext _context;
        public HomeController(masterDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {

            // 取得所有的 BookKeeping 資料
            var bookKeepings = _context.BookKeepings.ToList();
            ViewBag.BookKeepings = bookKeepings;

            return View();
        }

        public IActionResult List()
        {
            var bookKeepings = _context.BookKeepings.ToList();
            ViewBag.BookKeepings = bookKeepings;


            return View(bookKeepings);
        }

        public IActionResult Create(BookKeeping bk)
        {

            if (ModelState.IsValid)
            {
                _context.BookKeepings.Add(bk);
                _context.SaveChanges();

                ViewData["Message"] = "存檔成功";
            }

            return RedirectToAction("Index");
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
