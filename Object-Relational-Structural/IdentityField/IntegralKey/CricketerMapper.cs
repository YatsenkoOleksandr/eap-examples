using System.Data;

namespace IdentityField.IntegralKey;

/// <summary>
/// Data Mapper for <see cref="Cricketer"/>
/// </summary>
public class CricketerMapper : Mapper
{
    public Cricketer? Find(long id)
    {
        var found = AbstractFind(id);
        if (found is null)
        {
            return null;
        }

        return (Cricketer)found;
    }

    protected override DomainObject CreateDomainObject()
    {
        return new Cricketer();
    }

    protected override long GetNextID()
    {
        throw new NotImplementedException();
    }

    protected override void Load(DomainObject obj, DataRow row)
    {
        throw new NotImplementedException();
    }

    protected override void Save(DomainObject obj, DataRow row)
    {
        throw new NotImplementedException();
    }
}