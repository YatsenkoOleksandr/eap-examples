namespace AssociationTableMapping;

public abstract class DomainObject
{
    public const long PLACEHOLDER_ID = -1;

    public long Id { get; set; }
}
