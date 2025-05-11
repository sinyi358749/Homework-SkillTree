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

        public async Task<IPagedList<BookKeepingViewModel>> GetPagedBookKeepingAsync(int pageNumber = 1, int pageSize = 10)
        {
            var allItems = await GetAllBookKeepingAsync();
            return allItems.ToPagedList(pageNumber, pageSize);
        }

        //取清單
        public async Task<List<BookKeepingViewModel>> GetAllBookKeepingAsync()
        {
            var bookKeepings = new List<BookKeepingViewModel>();

            var result = await _accountBookRepository.GetAllAccountBooksAsync();

            result.ToList().ForEach(x =>
            {
                var bookKeeping = new BookKeepingViewModel
                {
                    keyId = x.Id,
                    Category = (x.Categoryyy == 0 ? "支出" : "收入"),
                    Amount = x.Amounttt,
                    TransDate = x.Dateee,
                    Description = x.Remarkkk
                };
                bookKeepings.Add(bookKeeping);
            });

            //依TransDate 排序
            bookKeepings = bookKeepings.OrderByDescending(x => x.TransDate).ToList();

            return bookKeepings;
        }

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
