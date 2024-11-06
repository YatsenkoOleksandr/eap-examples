namespace ForeignKeyMapping.SingleValue;

/// <summary>
/// Layer supertype for domain class
/// </summary>
public abstract class DomainObject
{
    public int Id { get; set; }
}