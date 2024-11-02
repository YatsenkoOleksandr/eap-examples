namespace IdentityField.CompoundKey;

/// <summary>
/// Extension methods to extract appropriate values from line item's key
/// </summary>
public static class LineItemKeyExtension
{
    public static long GetLineItemOrderId(this Key key)
    {
        return key.GetAsLongValue(0);
    }

    public static long GetLineItemSeq(this Key key)
    {
        return key.GetAsLongValue(1);
    }
}