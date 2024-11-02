namespace IdentityField.CompoundKey;

/// <summary>
/// Domain class with compound key (orderID, seq)
/// </summary>
public class LineItem: DomainObject
{
    public int Amount { get; set; }

    public string Product { get; set; } = string.Empty;

    public long? OrderId => Key?.GetAsLongValue(0);

    public long? Seq => Key?.GetAsLongValue(1);
}