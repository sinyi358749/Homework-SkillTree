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
        private readonly int PageSize = 10; // �C����ܼƶq


        public HomeController(ILogger<HomeController> logger, IBookKeepingService bookKeeping)
        {
            _bookKeepingService = bookKeeping;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            // ���o�Ҧ��� BookKeeping ���
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
                    ViewData["Message"] = "�s�ɦ��\";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Message"] = "�s�ɥ���";
                }
            }
            else
            {
                if (TempData["pageNumber"] != null)
                {//���o��e�����Ҧb��m
                    pageNumber = Convert.ToInt32(TempData["pageNumber"]);
                }
            
                //���o���ҿ��~�T��
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                var message = "";
                foreach (var error in errors)
                {
                    message += (message==""?"":" , ") +error.ErrorMessage ;
                }
                ViewData["Message"] = message;

                // �N����ƫO�s�� ViewData �� TempData
                ViewData["FormData"] = BookKeeping;
            }

            // ���s���J��U�������
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
