using System.Data;

namespace DependentMapping;

public abstract class AbstractMapper<IDomain, Key> where IDomain : IDomainObject<Key>
{
    protected abstract IDomain DoLoad(Key id, IDataReader row);

    public abstract void Update(IDomain domainObject);
}

