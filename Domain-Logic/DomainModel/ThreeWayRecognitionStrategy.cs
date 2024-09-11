namespace DomainModel;

internal class ThreeWayRecognitionStrategy: RecognitionStrategy
{
  private readonly int firstRecognitionOffset;
  private readonly int secondRecognitionOffset;

  public ThreeWayRecognitionStrategy(int firstRecognitionOffset, int secondRecognitionOffset)
  {
    this.firstRecognitionOffset = firstRecognitionOffset;
    this.secondRecognitionOffset = secondRecognitionOffset;
  }

  public override void CalculateRevenueRecognitions(Contract contract)
  {
    decimal[] allocation = Money.Allocate(contract.Revenue, 3);

    contract.AddRevenueRecognition(new RevenueRecognition(allocation[0], contract.WhenSigned));
    contract.AddRevenueRecognition(new RevenueRecognition(allocation[1], contract.WhenSigned.AddDays(firstRecognitionOffset)));
    contract.AddRevenueRecognition(new RevenueRecognition(allocation[2], contract.WhenSigned.AddDays(secondRecognitionOffset)));
  }
}