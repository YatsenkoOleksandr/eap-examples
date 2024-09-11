namespace DomainModel;

internal class Contract
{
  public Product Product { get; private set; }

  public decimal Revenue { get; private set; }

  public DateTime WhenSigned { get; private set; }

  public long Id { get; private set; }

  private readonly List<RevenueRecognition> revenueRecognitions = new List<RevenueRecognition>();

  public Contract(Product product, decimal revenue, DateTime whenSigned)
  {
    Product = product;
    Revenue = revenue;
    WhenSigned = whenSigned;
  }

  public void AddRevenueRecognition(RevenueRecognition revenueRecognition)
  {
    revenueRecognitions.Add(revenueRecognition);
  }

  public decimal RecognizedRevenue(DateTime asOf)
  {
    decimal result = 0;
    foreach (var revenueRecognition in revenueRecognitions)
    {
      if (revenueRecognition.IsRecognizableBy(asOf))
      {
        result += revenueRecognition.Amount;
      }
    }
    return result;
  }

  public void CalculateRevenueRecognitions()
  {
    Product.CalculateRevenueRecognitions(this);
  }
}