using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace DataMapper;

internal class DB
{
  private static SqlConnection dbConnection;

  public static DbCommand CreateCommand(string sqlQuery)
  {
    return new SqlCommand(sqlQuery, dbConnection);
  }

  public static DbParameter CreateParameter(string name, object value)
  {
    return new SqlParameter(name, value);
  }

  public static void Dispose(params IDisposable?[] dbResources)
  {
    foreach (var dbResource in dbResources)
    {
      dbResource?.Dispose();
    }
  }
}