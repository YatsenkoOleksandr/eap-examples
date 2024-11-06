using System.Collections;
using System.Data;

namespace ForeignKeyMapping.CollectionOfReferences;

public abstract class AbstractMapper<T> where T: DomainObject
{
    private readonly IDictionary<int, T> identityMap = new Dictionary<int, T>();

    private DataSetHolder dataSetHolder;

    protected abstract string TableName { get; }

    protected abstract void DoLoad(T domainObject, DataRow row);

    protected abstract T CreateDomainObject();

    protected abstract void Save(T domainObject, DataRow row);

    protected T Load(DataRow row)
    {
        int id = (int)row["id"];
        if (identityMap.TryGetValue(id, out T? domainObject))
        {
            return domainObject;
        }

        domainObject = CreateDomainObject();
        domainObject.Id = id;
        identityMap.Add(id, domainObject);
        DoLoad(domainObject, row);
        return domainObject;
    }

    protected T? AbstractFind(int id)
    {
        if (id == DomainObject.PLACEHOLDER_ID)
        {
            throw new ArgumentException("Should be a valid key", nameof(id));
        }

        var row = FindRow(id);
        if (row is null)
        {
            return null;
        }
        return Load(row);
    }

    protected DataRow? FindRow(int id)
    {
        string filter = $"id = {id}";
        var result = Table.Select(filter);

        return (result.Length == 0) ? null : result[0];
    }

    protected DataTable? Table
    {
        get { return dataSetHolder.Data.Tables[TableName]; }
    }

    public virtual void Update(T domainObject)
    {
        Save(domainObject, FindRow(domainObject.Id));
    }
}