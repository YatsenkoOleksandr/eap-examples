using System.Data;

namespace TableModule;

class RevenueRecognition: TableModule
{
  public RevenueRecognition(DataSet dataSet): base(dataSet, "RevenueRecognitions")
  {
  }

  public long Insert(long contractID, decimal amount, DateTime date)
  {
    DataRow newRow = table.NewRow();
    long id = GetNextID();
    newRow["ID"] = id;
    newRow["contractID"] = contractID;
    newRow["amount"] = amount;
    newRow["date"]= string.Format("{0:s}", date);
    table.Rows.Add(newRow);
    return id;
  }

  public decimal RecognizedRevenue(long contractID, DateTime asOf)
  {
    string filter = string.Format("ContractID = {0} AND date <= #{1:d}#", contractID, asOf);
    DataRow[] rows = table.Select(filter);
    decimal result = 0m;
    foreach (DataRow row in rows)
    {
      result += (decimal)row["amount"];
    }
  return result;
  }

  public decimal RecognizedRevenue2(long contractID, DateTime asOf)
  {
    /*
      Example using ADO.NET and aggregate function
    */

    string filter = string.Format("ContractID = {0} AND date <= #{1:d}#", contractID, asOf);
    string computeExpression = "sum(amount)";
    var sum = table.Compute(computeExpression, filter);
    return (sum is System.DBNull) ? 0 : (decimal) sum;
  }

  private long GetNextID()
  {
    // return 0 just for an example
    return 0;
  } 
}