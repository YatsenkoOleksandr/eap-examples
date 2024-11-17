using System.Data;
using System.Diagnostics;

namespace AssociationTableMapping;

public abstract class AbstractMapper<T> where T: DomainObject
{
    protected abstract string TableName { get; }

    protected abstract T CreateDomainObject();

    protected abstract void DoLoad(T domainObject, DataRow row);

    protected abstract void Save(T domainObject, DataRow row);

    protected DataSetHolder _dsh;

    protected Dictionary<long, T> _identityMap = [];
    
    protected DataTable Table
    {
        get { return _dsh.Data.Tables[TableName]; }
    }

    protected T? AbstractFind(long id)
    {
        Debug.Assert(id != DomainObject.PLACEHOLDER_ID);
        DataRow row = FindRow(id);
        if (row is null)
        {
            return null;
        }

        return Load(row);
    }

    protected DataRow? FindRow(long id)
    {
        var filter = $"id = {id}";
        var results = Table.Select(filter);

        if (results.Length == 0)
        {
            return null;
        }
        return results[0];
    }

    protected T Load(DataRow row)
    {
        long id = (long)row["id"];
        if (_identityMap[id] != null)
        {
            return _identityMap[id];
        }

        T result = CreateDomainObject();
        result.Id = id;
        _identityMap.Add(id, result);
        DoLoad(result, row);
        
        return result;
    }

    public virtual void Update(T domainObject)
    {
        Save(domainObject, FindRow(domainObject.Id));
    }
}