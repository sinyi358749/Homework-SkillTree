using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;
using Homework_SkillTree.Service;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookKeepingService  _bookKeepingService;
        private readonly int PageSize = 10; // �C����ܼƶq


        public HomeController(ILogger<HomeController> logger,IBookKeepingService bookKeeping)
        {
            _bookKeepingService = bookKeeping;
            _logger = logger;
        }

        public IActionResult Index(int page=1)
        {
            // ���o�Ҧ��� BookKeeping ���
            var bookKeepings = _bookKeepingService.GetAllBookKeepingAsync().Result;

            // ���Y�B�����޿�
            var count = bookKeepings.Count();
            var totalPage = (int)Math.Ceiling((double)count / PageSize);

            var startIndex = (page - 1) * PageSize;
            var endIndex = Math.Min(startIndex + PageSize, count);
            var paginatedBookKeepings = bookKeepings.Skip(startIndex).Take(PageSize).ToList();

            // �]�w������T
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPage;

            return View(paginatedBookKeepings);
        }


        [HttpPost]
        public IActionResult Create(BookKeepingViewModel bk)
        {

            if (ModelState.IsValid)
            {
                var result = _bookKeepingService.AddBookKeepingAsync(bk);
                if(result.Result)
                {
                    ViewData["Message"] = "�s�ɦ��\";
                }
                else
                {
                    ViewData["Message"] = "�s�ɥ���";
                }
            }

            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(Guid id)
        {
            var result = _bookKeepingService.DeleteBookKeepingAsync(id);
            if (result.Result)
            {
                ViewData["Message"] = "�R�����\";
            }
            else
            {
                ViewData["Message"] = "�R������";
            }
            return RedirectToAction("Index");
        }
    }
}
