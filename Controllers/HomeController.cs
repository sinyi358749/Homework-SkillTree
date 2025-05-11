using Homework_SkillTree.Models;
using Homework_SkillTree.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookKeepingService _bookKeepingService;
        private readonly int PageSize = 10; // 每頁顯示數量


        public HomeController(ILogger<HomeController> logger, IBookKeepingService bookKeeping)
        {
            _bookKeepingService = bookKeeping;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            // 取得所有的 BookKeeping 資料
            var model = await _bookKeepingService.GetPagedBookKeepingAsync(pageNumber, pageSize);


            TempData["pageNumber"] = pageNumber;
            TempData.Keep("pageNumber");

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index(BookKeepingViewModel BookKeeping)
        {
            var pageNumber = 1;

            if (ModelState.IsValid)
            {
                var result = await _bookKeepingService.AddBookKeepingAsync(BookKeeping);
                if (result)
                {
                    ViewData["Message"] = "存檔成功";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = "存檔失敗";
                }
            }
            else
            {
                if (TempData["pageNumber"] != null)
                {//取得當前分頁所在位置
                    pageNumber = Convert.ToInt32(TempData["pageNumber"]);
                }
            
                //取得驗證錯誤訊息
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var message = "";
                foreach (var error in errors)
                {
                    message += (message==""?"":" , ") +error.ErrorMessage ;
                }
                ViewData["Message"] = message;

                // 將表單資料保存到 ViewData 或 TempData
                ViewData["FormData"] = BookKeeping;
            }

            // 重新載入當下分頁資料
            var model = await _bookKeepingService.GetPagedBookKeepingAsync(pageNumber, PageSize);

            return View("Index", model);

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
