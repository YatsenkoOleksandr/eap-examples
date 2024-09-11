using Microsoft.Data.SqlClient;

namespace TransactionScript
{
  /*
    Transaction Script using Table Data Gateway
  */
  internal class RecognitionService
  {
    private readonly TableDataGateway db;

    public RecognitionService(TableDataGateway db)
    {
      this.db = db;
    }

    public decimal RecognizedRevenue(long contractNumber, DateTime asOf)
    {
      decimal result = 0;
      try
      {
        var reader = db.FindRecognitionsFor(contractNumber, asOf);
        while (reader.Read())
        {
          result += (decimal)reader["amount"];
        }
        return result;
      }
      catch (Exception e)
      {
        throw new ApplicationException($"Error occured while getting recognized revenue for contract #{contractNumber}", e);
      }
    }

    public void CalculateRevenueRecognitions(long contractNumber)
    {
      try
      {
        var contract = db.FindContract(contractNumber);
        contract.Read();

        var totalRevenue = (decimal)contract["revenue"];
        var recognitionDate = contract.GetDateTime(contract.GetOrdinal("dateSigned"));
        var type = contract["type"].ToString();

        if (type == "S")
        {
          var allocation = Money.Allocate(totalRevenue, 3);
          db.InsertRecognition(contractNumber, allocation[0], recognitionDate);
          db.InsertRecognition(contractNumber, allocation[1], recognitionDate.AddDays(60));
          db.InsertRecognition(contractNumber, allocation[0], recognitionDate.AddDays(90));
        }
        else if (type == "W")
        {
          db.InsertRecognition(contractNumber, totalRevenue, recognitionDate);
        }
        else if (type == "D")
        {
          var allocation = Money.Allocate(totalRevenue, 3);
          db.InsertRecognition(contractNumber, allocation[0], recognitionDate);
          db.InsertRecognition(contractNumber, allocation[1], recognitionDate.AddDays(30));
          db.InsertRecognition(contractNumber, allocation[0], recognitionDate.AddDays(60));
        }
      }
      catch (Exception e)
      {
        throw new ApplicationException($"Error occured while calculating revenue recognitions for contract #{contractNumber}", e);
      }
    }
  }
}