using Homework_SkillTree.Models;
using Homework_SkillTree.Data;
namespace Homework_SkillTree.Service
{
    public class BookKeepingService : IBookKeepingService
    {

        private readonly AccountBookRepository _accountBookRepository;

        public BookKeepingService(AccountBookRepository accountBookRepository)
        {
            _accountBookRepository = accountBookRepository;
        }

        //取清單
        public  Task<List<BookKeepingViewModel>> GetAllBookKeepingAsync()
        {
            var bk = new List<BookKeepingViewModel>();

            var result = _accountBookRepository.GetAllAccountBooksAsync();

            result.Result.ToList().ForEach(x =>
            {
                var bookKeeping = new BookKeepingViewModel
                {
                    keyId = x.Id,
                    Category = (x.Categoryyy==0?"支出":"收入"),
                    Amount = x.Amounttt,
                    TransDate = x.Dateee,
                    Description = x.Remarkkk
                };
                bk.Add(bookKeeping);
            });

            return Task.FromResult(bk.ToList());
        }

        //取單筆
        public Task<BookKeepingViewModel> GetBookKeepingByIdAsync(Guid id)
        {
            var bk = new BookKeepingViewModel();
            var result = _accountBookRepository.GetAccountBookByIdAsync(id);
            if (result.Result != null)
            {
                bk.keyId = result.Result.Id;
                bk.Category = (result.Result.Categoryyy == 0 ? "支出" : "收入");
                bk.Amount = result.Result.Amounttt;
                bk.TransDate = result.Result.Dateee;
                bk.Description = result.Result.Remarkkk;
            }
            return Task.FromResult(bk);
        }

        public Task<bool> AddBookKeepingAsync(BookKeepingViewModel bookKeeping)
        {
            var AccountBookModel = new AccountBookModel
            {
                Id = Guid.NewGuid(),
                Categoryyy = bookKeeping.Category == "支出" ? 0 : 1,
                Amounttt = Convert.ToInt32(Math.Round(bookKeeping.Amount ?? 0)),
                Dateee = bookKeeping.TransDate ?? DateTime.Now,
                Remarkkk = bookKeeping.Description ?? string.Empty,
            };

            var result = _accountBookRepository.AddAccountBooksAsync(AccountBookModel);
            return Task.FromResult(result.Result > 0);
        }

        public Task<bool> UpdateBookKeepingAsync(BookKeepingViewModel bookKeeping)
        {
            var AccountBookModel = new AccountBookModel();
            AccountBookModel.Id = bookKeeping.keyId;
            AccountBookModel.Categoryyy = bookKeeping.Category == "支出" ? 0 : 1;
            AccountBookModel.Amounttt = Convert.ToInt32(Math.Round(bookKeeping.Amount ?? 0));
            AccountBookModel.Dateee = bookKeeping.TransDate ?? DateTime.Now;
            AccountBookModel.Remarkkk = bookKeeping.Description ?? string.Empty;
       
            var result = _accountBookRepository.UpdateAccountBooksAsync(AccountBookModel);
            return Task.FromResult(result.Result > 0);
        }

        public Task<bool> DeleteBookKeepingAsync(Guid id)
        {
           var result = _accountBookRepository.DeleteAccountBooksAsync(id);
            return Task.FromResult(result.Result > 0);
        }

    }
}
