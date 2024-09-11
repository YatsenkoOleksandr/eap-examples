namespace ServiceLayer;

internal interface IIntegrationGateway
{
  void PublishRevenueRecognitionCalculation(Contract contract);
}

/*
  Integration Gateway without implementation
*/
internal class FakeIntegrationGateway : IIntegrationGateway
{
  public void PublishRevenueRecognitionCalculation(Contract contract)
  {
  }
}