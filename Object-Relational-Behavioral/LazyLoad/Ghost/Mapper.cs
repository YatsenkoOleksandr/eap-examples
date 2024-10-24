using System.Collections;
using System.Data;
using Microsoft.Data.SqlClient;

namespace LazyLoad.Ghost;

/// <summary>
/// Abstract Data Mapper 
/// </summary>
public abstract class Mapper
{
    /// <summary>
    /// Identity Map to store loaded domain objects
    /// </summary>
    private readonly Dictionary<int, DomainObject> loadedMap = [];

    /// <summary>
    /// Returns domain object in Ghost state
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected abstract DomainObject CreateGhost(int key);

    /// <summary>
    /// Loads the <paramref name="obj"/> from <paramref name="reader"/>
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="obj"></param>
    protected abstract void DoLoad(IDataReader reader, DomainObject obj);

    /// <summary>
    /// Returns domain object in ghost state
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected DomainObject AbstractFind(int key)
    {
        var alreadyLoaded = loadedMap.TryGetValue(key, out DomainObject result);
        if (!alreadyLoaded)
        {
            result = CreateGhost(key);
            loadedMap.Add(key, result);
        }

        return result;
    }

    /// <summary>
    /// Actual method which loads the rest of the domain object
    /// </summary>
    /// <param name="obj"></param>
    public void Load(DomainObject obj)
    {
        if (!obj.IsGhost)
        {
            return;
        }

        var command = new SqlCommand("", DB.Connection);
        command.Parameters.Add(new SqlParameter("key", obj.Key));
        IDataReader reader = command.ExecuteReader();
        reader.Read();

        if (obj.IsGhost)
        {
            obj.MarkAsLoading();
            DoLoad(reader, obj);
            obj.MarkAsLoaded();
        }

        reader.Close();
        reader.Dispose();
    }
}