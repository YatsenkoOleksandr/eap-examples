namespace ServiceLayer;

/**
  Service Layer extening Layer Supertype

	Script the application logic of the operations, delegating to domain object classes
*/
internal class RecognitionService : ApplicationService
{
  public decimal RecognizedRevenue(long contractNumber, DateTime asOf)
  {
    return Contract.Read(contractNumber).RecognizedRevenue(asOf);
  }

  public void CalculateRevenueRecognitions(long contractNumber)
  {
    Contract contract = Contract.ReadForUpdate(contractNumber);
    contract.CalculateRecognitions();

    this.GetEmailGateway().SendEmailMessage(
      contract.GetAdministratorEmailAddress(),
      "RE: Contract #" + contractNumber,
      contract + " has had revenue recognitions calculated."
    );
    this.GetIntegrationGateway().PublishRevenueRecognitionCalculation(contract);
  }
}