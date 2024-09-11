namespace ServiceLayer;

/* Domain Model without implementation */
internal class Contract
{
  public Contract()
  {    
  }

  public static Contract ReadForUpdate(long contractNumber)
  {
    //
    throw new NotImplementedException();
  }

  public static Contract Read(long contractNumber)
  {
    //
    throw new NotImplementedException();
  }

  public void CalculateRecognitions()
  {
    //
    throw new NotImplementedException();
  }

  public string GetAdministratorEmailAddress()
  {
    //
    throw new NotImplementedException();
  }

  public decimal RecognizedRevenue(DateTime asOf)
  {
    //
    throw new NotImplementedException();
  }
}