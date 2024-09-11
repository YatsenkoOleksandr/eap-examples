namespace ServiceLayer;

/*
  Layer Supertype which provides gateways
*/
internal class ApplicationService
{
  protected IEmailGateway GetEmailGateway()
  {
    return new FakeEmailGateway();
  }

  protected IIntegrationGateway GetIntegrationGateway()
  {
    return new FakeIntegrationGateway();
  }
}