namespace ServiceLayer;

internal interface IEmailGateway
{
  public void SendEmailMessage(string toAddress, string subject, string body);
}

/*
  Email Gateway without implementation
*/
internal class FakeEmailGateway : IEmailGateway
{
  public void SendEmailMessage(string toAddress, string subject, string body)
  {
  }
}
