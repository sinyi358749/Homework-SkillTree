using Homework_SkillTree.Models;
using X.PagedList;

//實作 IBookKeepingService 介面，負責與資料庫進行互動
namespace Homework_SkillTree.Service
{
    public interface IBookKeepingService
    {
       
        Task<IPagedList<BookKeepingViewModel>> GetPagedBookKeepingAsync(int pageNumber = 1, int pageSize = 10);

        Task<List<BookKeepingViewModel>> GetAllBookKeepingAsync();

        Task<BookKeepingViewModel> GetBookKeepingByIdAsync(Guid id);

        Task<bool> AddBookKeepingAsync(BookKeepingViewModel bookKeeping);

        Task<bool> UpdateBookKeepingAsync(BookKeepingViewModel bookKeeping);

        Task<bool> DeleteBookKeepingAsync(Guid id);

    }
}
