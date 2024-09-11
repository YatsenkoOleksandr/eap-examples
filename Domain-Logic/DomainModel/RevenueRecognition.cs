namespace DomainModel;

internal class RevenueRecognition
{
  public decimal Amount { get; private set; }

  public DateTime Date { get; private set; }

  public RevenueRecognition(decimal amount, DateTime date)
  {
    Amount = amount;
    Date = date;
  }

  public bool IsRecognizableBy(DateTime asOf)
  {
    return asOf >= Date;
  }
}
