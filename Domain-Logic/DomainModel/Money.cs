namespace DomainModel
{
  /*
    Stub for Money pattern to carry out money split
  */
  internal class Money
  {
    public static decimal[] Allocate(decimal amount, int by)
    {
      decimal lowResult = amount / by;
      lowResult = Decimal.Round(lowResult, 2);
      decimal highResult = lowResult + 0.01m;

      decimal[] results = new decimal[by];

      int remainder = (int)amount % by;
      for (int i = 0; i < remainder; i++)
      {
        results[i] = highResult;
      }
      for (int i = remainder; i < by; i++)
      {
        results[i] = lowResult;
      }
      return results;
    }
  }
}