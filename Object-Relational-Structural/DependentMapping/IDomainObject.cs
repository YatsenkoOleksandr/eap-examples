namespace DependentMapping;

public interface IDomainObject<Key>
{
    public Key Id { get; set; }
}
