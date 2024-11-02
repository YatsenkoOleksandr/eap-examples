using System.Data;
using Microsoft.Data.SqlClient;

namespace IdentityField.CompoundKey;

public abstract class AbstractMapper
{
    protected Dictionary<Key, DomainObject> loadedMap = [];

    public DomainObject AbstractFind(Key key)
    {
        var found = loadedMap.TryGetValue(key, out var result);
        if (found)
        {
            return result;
        }

        throw new NotImplementedException("Not implemented for example simplicity");
    }

    public virtual Key Insert(DomainObject domainObject)
    {
        return PeformInsert(domainObject, FindNextDatabaseKeyObject());
    }

    public virtual void Update(DomainObject domainObject)
    {
        var updateCommand = CreateUpdateCommand();
        LoadUpdateCommand(domainObject, updateCommand);
        updateCommand.ExecuteNonQuery();

        // Map update was missing in the original example
        if (loadedMap.ContainsKey(domainObject.Key))
        {
            loadedMap[domainObject.Key] = domainObject;
        }
        else
        {
            loadedMap.Add(domainObject.Key, domainObject);
        }
    }

    public virtual void Delete(DomainObject domainObject)
    {
        var deleteCommand = CreateDeleteCommand();
        LoadDeleteCommand(domainObject, deleteCommand);
        deleteCommand.ExecuteNonQuery();
        loadedMap.Remove(domainObject.Key);
    }

    #region Load domain object from SQL result set

    protected abstract string GetFindQuery();

    /// <summary>
    /// Hook method to add key as parameter. Mappers for domain class with compound key should override it.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="findCommand"></param>
    protected virtual void AddKeyParameterToQuery(Key key, SqlCommand findCommand)
    {
        findCommand.Parameters.AddWithValue("@id", key.GetAsLongValue());
    }

    protected DomainObject Load(IDataReader reader)
    {
        Key key = CreateKey(reader);
        if (loadedMap.TryGetValue(key, out DomainObject? value))
        {
            return value;
        }

        value = DoLoad(key, reader);
        loadedMap.Add(key, value);
        return value;
    }

    /// <summary>
    /// Hook method to extract key from result set. Mapper for domain class with compound key should override it.
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected virtual Key CreateKey(IDataReader reader)
    {
        long id = reader.GetInt64(reader.GetOrdinal("id"));
        return new Key(id);
    }

    /// <summary>
    /// Method to load domain object from result set
    /// </summary>
    /// <param name="key"></param>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected abstract DomainObject DoLoad(Key key, IDataReader reader);
    #endregion

    #region Insert Methods
    protected abstract Key FindNextDatabaseKeyObject();

    protected Key PeformInsert(DomainObject domainObject, Key key)
    {
        domainObject.Key = key;

        var command = CreateInsertCommand();
        InsertKey(domainObject, command);
        InsertData(domainObject, command);
        command.ExecuteNonQuery();

        loadedMap.Add(key, domainObject);
        return domainObject.Key;
    }

    protected abstract SqlCommand CreateInsertCommand();

    /// <summary>
    /// Hook method to set key value in the insert statement.
    /// Mappers for domain class with compound key should override it
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="sqlCommand"></param>
    protected virtual void InsertKey(DomainObject domainObject, SqlCommand sqlCommand)
    {
        sqlCommand.Parameters.AddWithValue("@id", domainObject.Key.GetAsLongValue());
    }

    /// <summary>
    /// Hooke method to set rest of the domain object in the insert statement.
    /// </summary>
    /// <param name="domainObject"></param>
    /// <param name="sqlCommand"></param>
    protected abstract void InsertData(DomainObject domainObject, SqlCommand sqlCommand);
    #endregion

    #region Update methods

    protected abstract SqlCommand CreateUpdateCommand();

    protected abstract void LoadUpdateCommand(DomainObject domainObject, SqlCommand command);
    #endregion

    #region Delete Methods
    protected abstract SqlCommand CreateDeleteCommand();

    protected abstract void LoadDeleteCommand(DomainObject domainObject, SqlCommand sqlCommand);
    #endregion
}