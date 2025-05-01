using System.Data;
using Microsoft.Data.SqlClient; 

namespace Homework_SkillTree.Data
{
    public class DapperDbContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // 提供 IDbConnection 實例
        public IDbConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    _connection = new SqlConnection(_connectionString);
                }
                return _connection;
            }
        }

        // 釋放資源
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}