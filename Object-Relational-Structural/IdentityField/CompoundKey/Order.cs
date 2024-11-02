namespace IdentityField.CompoundKey;

/// <summary>
/// Domain class with single integral key (id)
/// </summary>
public class Order: DomainObject
{
    public string Customer { get; set; } = string.Empty;

    /// <summary>
    /// Helper property
    /// </summary>
    public long? Id => Key?.GetAsLongValue();
}