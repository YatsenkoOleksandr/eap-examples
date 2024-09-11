using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace TableDataGateway.ADO.NET.DATASET;

/*
  Gateway uses holder to store the data set.
  Holder indexes the data sets and adapters by the table name
*/
internal class DataSetHolder
{
  public DataSet Data = new DataSet();
  private Hashtable DataAdapters = new Hashtable();

  public void FillData(string query, string tableName)
  {
    if (DataAdapters.Contains(tableName))
    {
      throw new Exception();
    };

    OleDbDataAdapter da = new OleDbDataAdapter(query, DB.Connection);
    OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
    da.Fill(Data, tableName);
    DataAdapters.Add(tableName, da);
  }

  public void Update()
  {
    foreach (string table in DataAdapters.Keys)
    {
      ((OleDbDataAdapter)DataAdapters[table]).Update(Data, table);
    }
  }

  public DataTable this[String tableName]
  {
    get { return Data.Tables[tableName]; }
  }
}