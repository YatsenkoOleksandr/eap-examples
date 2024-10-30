namespace IdentityField.IntegralKey;

/// <summary>
/// Supertype Domain Object with Identity Field
/// </summary>
public class DomainObject
{
    public const long PLACEHOLDER_ID = -1;

    /// <summary>
    /// Identity Field - Integral Key
    /// </summary>
    public long Id { get; set; } = PLACEHOLDER_ID;

    public bool IsNew { get { return Id == PLACEHOLDER_ID;} }
}
