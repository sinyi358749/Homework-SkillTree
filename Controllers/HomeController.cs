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

        // ��������
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

            // ���o�Ҧ��� BookKeeping ���
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


            return RedirectToAction("Index"); ;
        }

        public async Task<IActionResult>  Edit(Guid id)
        {
            var result = await _bookKeepingService.GetBookKeepingByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            // �O�s�n�s�誺��ƨ� ViewData
            ViewData["FormData"] = result;
            ViewData["IsEdit"] = true;

            // ���o��e���������
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
