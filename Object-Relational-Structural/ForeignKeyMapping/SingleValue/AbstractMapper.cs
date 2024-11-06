using System.Data;

namespace ForeignKeyMapping.SingleValue;

public abstract class AbstractMapper
{
    public virtual DomainObject AbstractFind(int id)
    {
        throw new NotImplementedException();
        return DoLoad(id, null);
    }

    public abstract void Update(DomainObject domainObject);

    protected abstract DomainObject DoLoad(int id, IDataReader reader);
}