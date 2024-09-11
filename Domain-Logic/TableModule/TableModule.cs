using System.Data;

namespace TableModule;

internal abstract class TableModule
{
  protected DataTable table;

  protected TableModule(DataSet dataSet, string tableName)
  {
    table = dataSet.Tables[tableName];
  }
}
