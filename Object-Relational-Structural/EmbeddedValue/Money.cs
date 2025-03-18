namespace EmbeddedValue
{
    /// <summary>
    /// Embedded Value to represent money, contains cost and currency
    /// </summary>
    public class Money
    {
        public decimal Amount { get; set; }

        public string? Currency { get; set; }
    }
}
