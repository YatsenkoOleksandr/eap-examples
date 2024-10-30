using System.Data;

namespace IdentityField.IntegralKey;

/// <summary>
/// Mapper Layer Supertype
/// </summary>
public abstract class Mapper
{
    protected abstract DomainObject CreateDomainObject();

    protected abstract void Load(DomainObject obj, DataRow row);

    protected abstract void Save(DomainObject obj, DataRow row);

    protected abstract long GetNextID();

    protected DataTable table;

    protected DomainObject? AbstractFind(long id)
    {
        var row = FindRow(id);

        return (row is null) ? null : Find(row);
    }

    protected DataRow? FindRow(long id)
    {
        var results = table.Select($"id = {id}");
        return (results.Length == 0) ? null : results[0];
    }

    protected DomainObject Find(DataRow row)
    {
        DomainObject result = CreateDomainObject();
        Load(result, row);
        return result;
    }

    public virtual long Insert(DomainObject obj)
    {
        DataRow row = table.NewRow();
        obj.Id = GetNextID();
        row["id"] = obj.Id;
        Save(obj, row);
        table.Rows.Add(row);
        return obj.Id;
    }
}