using System.Data;

namespace TableDataGateway.ADO.NET.DATASET;

abstract internal class DataGateway
{
  /*
    Gateway stores the holder and exposes the data set for its clients
  */

  public DataSetHolder Holder;
  public DataSet Data
  {
    get
    {
      return Holder.Data;
    }
  }

  /*
    Gateway can act on an existing holder, or it can create a new one
  */

  protected DataGateway()
  {
    Holder = new DataSetHolder();
  }
  
  protected DataGateway(DataSetHolder holder)
  {
    this.Holder = holder;
  }

  abstract public string TableName { get; }

  /*
    A data set is a container and can hold data from several tables,
    so it's better to hold data into a data set
  */

  public void LoadAll()
  {
    string commandString = string.Format("select * from {0}", TableName);
    Holder.FillData(commandString, TableName);
  }

  public void LoadWhere(string whereClause)
  {
    string commandString = string.Format(
      "select * from {0} where {1}",
      TableName,
      whereClause
    );
    Holder.FillData(commandString, TableName);
  }

  /*
    The gateway can have an indexer to make it easier to get to specific rows.
  */

  public DataRow this[long key]
  {
    get
    {
      string filter = string.Format("id = {0}", key);
      return Table.Select(filter)[0];
    }
  }
  public DataTable Table
  {
    get { return Data.Tables[TableName]; }
  }
}