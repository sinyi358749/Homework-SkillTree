using Homework_SkillTree.Models;

//實作 IBookKeepingService 介面，負責與資料庫進行互動
namespace Homework_SkillTree.Service
{
    public interface IBookKeepingService
    {
        Task<List<BookKeepingViewModel>> GetAllBookKeepingAsync();

        Task<BookKeepingViewModel> GetBookKeepingByIdAsync(Guid id);

        Task<bool> AddBookKeepingAsync(BookKeepingViewModel bookKeeping);

        Task<bool> UpdateBookKeepingAsync(BookKeepingViewModel bookKeeping);

        Task<bool> DeleteBookKeepingAsync(Guid id);

    }
}
