using Homework_SkillTree.Models;
using Homework_SkillTree.Service;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookKeepingService _bookKeepingService;
        private readonly IConfiguration _configuration;
 

        public HomeController(ILogger<HomeController> logger, IBookKeepingService bookKeeping, IConfiguration configuration)
        {
            _bookKeepingService = bookKeeping;
            _logger = logger;
            _configuration = configuration;
        }

        // 分頁筆數
        private int PageSize
        {
            get
            {
                return _configuration.GetValue<int>("PageSize");
            }
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = 1;
           if(page != null)
            {
                if (page < 1)
                {
                    pageNumber = 1;
                }
                else
                {
                    pageNumber = (int)page;
                }
            }

            // 取得所有的 BookKeeping 資料
            var model = await _bookKeepingService.GetPagedBookKeepingAsync(pageNumber, PageSize);

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


            return RedirectToAction("Index"); ;
        }

        public async Task<IActionResult>  Edit(Guid id)
        {
            var result = await _bookKeepingService.GetBookKeepingByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            // 保存要編輯的資料到 ViewData
            ViewData["FormData"] = result;
            ViewData["IsEdit"] = true;

            // 取得當前分頁的資料
            var pageNumber = 1;
            if (TempData["pageNumber"] != null)
            {
                pageNumber = Convert.ToInt32(TempData["pageNumber"]);
            }

            var model = await _bookKeepingService.GetPagedBookKeepingAsync(pageNumber, PageSize);
            return View("Index", model);
        }

    }
}
