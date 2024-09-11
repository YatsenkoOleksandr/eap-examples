using System.Data.OleDb;

namespace TableDataGateway.ADO.NET.DATASET;

internal class DB
{
  public static OleDbConnection Connection { get; private set; }
}