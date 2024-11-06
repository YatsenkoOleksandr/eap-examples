namespace ForeignKeyMapping.CollectionOfReferences;

/// <summary>
/// Layer supertype for domain class
/// </summary>
public abstract class DomainObject
{
    public const int PLACEHOLDER_ID = -1;

    public int Id { get; set; } = PLACEHOLDER_ID;
}