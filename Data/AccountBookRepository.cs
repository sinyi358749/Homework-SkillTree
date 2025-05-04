using Dapper;
using Homework_SkillTree.Models;

namespace Homework_SkillTree.Data
{

    public class AccountBookRepository
    {
        private readonly DapperDbContext _dbContext;

        public AccountBookRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AccountBookModel>> GetAllAccountBooksAsync()
        {
            var query = "SELECT * FROM AccountBook with(nolock)";
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<AccountBookModel>(query);
            }
        }

        public async Task<AccountBookModel> GetAccountBookByIdAsync(Guid id)
        {
            var query = "SELECT * FROM AccountBook with(nolock) WHERE Id = @Id";
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<AccountBookModel>(query, new { Id = id });
            }
        }

        public async Task<int> AddAccountBooksAsync(AccountBookModel account)
        {
            var query = "Insert into AccountBook  ([Id],[Categoryyy],[Amounttt],[Dateee],[Remarkkk]) Values (@Id,@Categoryyy,@Amounttt,@Dateee,@Remarkkk)";
            using (var connection = _dbContext.Connection)
            {
                return await connection.ExecuteAsync(query, account);
            }
        }

        public async Task<int> UpdateAccountBooksAsync(AccountBookModel account)
        {
            var query = "Update AccountBook set [Categoryyy]=@Categoryyy,[Amounttt]=@Amounttt,[Dateee]=@Dateee,[Remarkkk]=@Remarkkk where Id=@Id";
            using (var connection = _dbContext.Connection)
            {
                return await connection.ExecuteAsync(query, account);
            }
        }
                public async Task<int> DeleteAccountBooksAsync(Guid id)
        {
            var query = "Delete from AccountBook where Id=@Id";
            using (var connection = _dbContext.Connection)
            {
                return await connection.ExecuteAsync(query, new { Id = id });
            }
        }
    }
}
