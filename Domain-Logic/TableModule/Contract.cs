using System.Data;

namespace TableModule;

internal class Contract : TableModule
{
  public Contract(DataSet dataSet) : base(dataSet, "Contracts")
  {
  }

  // indexer, which gets to a particular row in the data table by the primary key
  public DataRow this[long key]
  {
    get
    {
      string filter = $"ID = {key}";
      return table.Select(filter)[0];
    }
  }

  public void CalculateRecognitions(long contractID)
  {
    DataRow contractRow = this[contractID];
    decimal amount = (decimal)contractRow["amount"];

    RevenueRecognition rr = new RevenueRecognition(table.DataSet);
    Product prod = new Product(table.DataSet);

    long prodID = GetProductID(contractID);
    if (prod.GetProductType(prodID) == ProductType.WP)
    {
      rr.Insert(contractID, amount, (DateTime)GetWhenSigned(contractID));
    }
    else if (prod.GetProductType(prodID) == ProductType.SS)
    {
      decimal[] allocation = Allocate(amount, 3);
      rr.Insert(contractID, allocation[0], (DateTime)GetWhenSigned(contractID));
      rr.Insert(contractID, allocation[1], ((DateTime)GetWhenSigned(contractID)).AddDays(60));
      rr.Insert(contractID, allocation[2], ((DateTime)GetWhenSigned(contractID)).AddDays(90));
    }
    else if (prod.GetProductType(prodID) == ProductType.DB)
    {
      decimal[] allocation = Allocate(amount, 3);
      rr.Insert(contractID, allocation[0], (DateTime)GetWhenSigned(contractID));
      rr.Insert(contractID, allocation[1], ((DateTime)GetWhenSigned(contractID)).AddDays(30));
      rr.Insert(contractID, allocation[2], ((DateTime)GetWhenSigned(contractID)).AddDays(60));
    }
    else throw new Exception("invalid product id");
  }

  private static decimal[] Allocate(decimal amount, int by)
  {
    decimal lowResult = amount / by;
    lowResult = decimal.Round(lowResult, 2);
    decimal highResult = lowResult + 0.01m;

    decimal[] results = new decimal[by];

    int remainder = (int)amount % by;
    for (int i = 0; i < remainder; i++)
    {
      results[i] = highResult;
    }
    for (int i = remainder; i < by; i++)
    {
      results[i] = lowResult;
    }
    return results;
  }

  private int GetProductID(long contractID)
  {
    DataRow contractRow = this[contractID];
    return (int)contractRow["productId"];
  }

  private object GetWhenSigned(long contractID)
  {
    DataRow contractRow = this[contractID];
    return contractRow["whenSigned"];
  }
}