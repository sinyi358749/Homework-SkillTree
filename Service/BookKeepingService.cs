using Homework_SkillTree.Models;
using Homework_SkillTree.Data;
using X.PagedList.Extensions;
using X.PagedList;
namespace Homework_SkillTree.Service
{
    public class BookKeepingService : IBookKeepingService
    {

        private readonly AccountBookRepository _accountBookRepository;

        public BookKeepingService(AccountBookRepository accountBookRepository)
        {
            _accountBookRepository = accountBookRepository;
        }

        public async Task<IPagedList<BookKeepingViewModel>> GetPagedBookKeepingAsync(int pageNumber , int pageSize)
        {
            var boolKeepings = new List<BookKeepingViewModel>();
            var result = await _accountBookRepository.GetAccountBooksWithTotalAsync(pageNumber, pageSize);
            //var result = await _accountBookRepository.GetAllAccountBooksAsync(pageNumber, pageSize);

            foreach (var bookKeepingViewModel in result.Data)
            {
                var bookKeeping = new BookKeepingViewModel
                {
                    keyId = bookKeepingViewModel.Id,
                    Category = (bookKeepingViewModel.Categoryyy == 0 ? "支出" : "收入"),
                    Amount = bookKeepingViewModel.Amounttt,
                    TransDate = bookKeepingViewModel.Dateee,
                    Description = bookKeepingViewModel.Remarkkk
                };
                boolKeepings.Add(bookKeeping);
            }
            boolKeepings = boolKeepings.OrderByDescending(x => x.TransDate).ToList();//`排序
            int totalCount = result.TotalCount;//取得總筆數

            return new StaticPagedList<BookKeepingViewModel>(boolKeepings, pageNumber, pageSize, totalCount);

        }

        //取清單
        //public async Task<List<BookKeepingViewModel>> GetAllBookKeepingAsync(int pageNumber = 1, int pageSize = 10)
        //{
        //    var bookKeepings = new List<BookKeepingViewModel>();

        //    var result = await _accountBookRepository.GetAllAccountBooksAsync(pageNumber, pageSize);

        //    result.ToList().ForEach(x =>
        //    {
        //        var bookKeeping = new BookKeepingViewModel
        //        {
        //            keyId = x.Id,
        //            Category = (x.Categoryyy == 0 ? "支出" : "收入"),
        //            Amount = x.Amounttt,
        //            TransDate = x.Dateee,
        //            Description = x.Remarkkk
        //        };
        //        bookKeepings.Add(bookKeeping);
        //    });

        //    //依TransDate 排序
        //    bookKeepings = bookKeepings.OrderByDescending(x => x.TransDate).ToList();

        //    return bookKeepings;
        //}

        //取單筆
        public async Task<BookKeepingViewModel> GetBookKeepingByIdAsync(Guid id)
        {
            var bookKeepings = new BookKeepingViewModel();
            var result = await _accountBookRepository.GetAccountBookByIdAsync(id);
            if (result != null)
            {
                bookKeepings.keyId = result.Id;
                bookKeepings.Category = (result.Categoryyy == 0 ? "支出" : "收入");
                bookKeepings.Amount = result.Amounttt;
                bookKeepings.TransDate = result.Dateee;
                bookKeepings.Description = result.Remarkkk;
            }
            return bookKeepings;
        }

        public async Task<bool> AddBookKeepingAsync(BookKeepingViewModel bookKeeping)
        {
            var AccountBookModel = new AccountBookModel
            {
                Id = Guid.NewGuid(),
                Categoryyy = bookKeeping.Category == "支出" ? 0 : 1,
                Amounttt = bookKeeping.Amount,
                Dateee = bookKeeping.TransDate ?? DateTime.Now,
                Remarkkk = bookKeeping.Description ?? string.Empty,
            };

            var result = await _accountBookRepository.AddAccountBooksAsync(AccountBookModel);

            return result > 0;
        }

        public async Task<bool> UpdateBookKeepingAsync(BookKeepingViewModel bookKeeping)
        {
            var AccountBookModel = new AccountBookModel();
            AccountBookModel.Id = bookKeeping.keyId;
            AccountBookModel.Categoryyy = bookKeeping.Category == "支出" ? 0 : 1;
            AccountBookModel.Amounttt = bookKeeping.Amount;
            AccountBookModel.Dateee = bookKeeping.TransDate ?? DateTime.Now;
            AccountBookModel.Remarkkk = bookKeeping.Description ?? string.Empty;

            var result = await _accountBookRepository.UpdateAccountBooksAsync(AccountBookModel);
            return result > 0;
        }

        public async Task<bool> DeleteBookKeepingAsync(Guid id)
        {
            var result = await _accountBookRepository.DeleteAccountBooksAsync(id);
            return result > 0;
        }

    }
}
