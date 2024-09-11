namespace DomainModel;

internal class CompleteRecognitionStrategy : RecognitionStrategy
{
  public override void CalculateRevenueRecognitions(Contract contract)
  {
    contract.AddRevenueRecognition(new RevenueRecognition(contract.Revenue, contract.WhenSigned));
  }
}