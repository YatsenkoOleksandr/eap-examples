namespace ForeignKeyMapping.CollectionOfReferences;

/// <summary>
/// Dependent Domain, but without a link
/// </summary>
public class Player: DomainObject
{
    public string Name { get; set; } = string.Empty;
}