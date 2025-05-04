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
        private readonly int PageSize = 10; // 每頁顯示數量


        public HomeController(ILogger<HomeController> logger,IBookKeepingService bookKeeping)
        {
            _bookKeepingService = bookKeeping;
            _logger = logger;
        }

        public IActionResult Index(int page=1)
        {
            // 取得所有的 BookKeeping 資料
            var bookKeepings = _bookKeepingService.GetAllBookKeepingAsync().Result;

            // 偷吃步分頁邏輯
            var count = bookKeepings.Count();
            var totalPage = (int)Math.Ceiling((double)count / PageSize);

            var startIndex = (page - 1) * PageSize;
            var endIndex = Math.Min(startIndex + PageSize, count);
            var paginatedBookKeepings = bookKeepings.Skip(startIndex).Take(PageSize).ToList();

            // 設定分頁資訊
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
                    ViewData["Message"] = "存檔成功";
                }
                else
                {
                    ViewData["Message"] = "存檔失敗";
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
                ViewData["Message"] = "刪除成功";
            }
            else
            {
                ViewData["Message"] = "刪除失敗";
            }
            return RedirectToAction("Index");
        }
    }
}
