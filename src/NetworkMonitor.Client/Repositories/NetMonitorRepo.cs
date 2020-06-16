using Dapper;
using NetworkMonitor.Client.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace NetworkMonitor.Client.Repositories
{
  public class NetMonitorRepo: INetMonitorRepo
  {
    private readonly string _connectionString;

    public NetMonitorRepo(ConnectionString connectionString)
    {
      _connectionString = connectionString.Default;
    }

    private IDbConnection GetConnection()
    {
      return new SqlConnection(_connectionString);
    }

    public async Task<bool> WriteBandwidthTestResults(BandwidthTestResult testResults)
    {
      using(var conn = GetConnection())
      {
        var sql = @"
          insert into NetTests
          (DownloadSpeed, UploadSpeed)
          values
          (@DownloadSpeed, @UploadSpeed)";

        conn.Open();

        var rowsAffected = await conn.ExecuteAsync(sql, testResults);

        return rowsAffected == 1;
      }
    }
  }
}
