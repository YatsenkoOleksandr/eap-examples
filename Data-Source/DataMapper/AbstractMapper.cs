using System.Data.Common;

namespace DataMapper;


/**
  Abstract Mapper - Layer Supertype to put common behavior.
  Uses an Identity Map to pull objects.
*/
internal abstract class AbstractMapper<T> where T: DomainObject
{
  /**
    Identity Map to track loaded objects
  */
  protected Dictionary<int, T> loadedMap = new Dictionary<int, T>();

  protected abstract string FindQuery();

  protected T AbstractFind(int id)
  {
    var found = loadedMap.TryGetValue(id, out T result);
    if (found)
    {
      return result;
    }

    DbCommand command = null;
    DbDataReader reader = null;
    try
    {
      command = DB.CreateCommand(FindQuery());
      command.Parameters.Add(DB.CreateParameter("@id", id));
      reader = command.ExecuteReader();
      reader.Read();

      result = Load(reader);
      return result;
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to get by #{id}.", ex);
    }
    finally
    {
      DB.Dispose(command, reader);
    }
  }

  protected IEnumerable<T> FindMany(StatementSource statementSource)
  {
    DbCommand command = null;
    DbDataReader reader = null;
    try
    {
      command = DB.CreateCommand(statementSource.Sql());
      foreach (var param in statementSource.Parameters())
      {
        command.Parameters.Add(param);
      }
      reader = command.ExecuteReader();
      return LoadAll(reader);
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to execute query '{statementSource.Sql}'", ex);
    }
    finally
    {
      DB.Dispose(command, reader);
    }
  }

  protected T Load(DbDataReader reader)
  {
    int id = reader.GetInt32(reader.GetOrdinal("id"));
    /* 
      Identity Map checked twice for a reason:
      there might be queries which return multiple records, so still need to query db
      and then double-check if record was already loaded before
    */
    if (loadedMap.TryGetValue(id, out T? value))
    {
      return value;
    }

    T result = DoLoad(id, reader);
    loadedMap.Add(id, result);
    return result;
  }

  protected IEnumerable<T> LoadAll(DbDataReader reader)
  {
    var list = new List<T>();
    while (reader.Read())
    {
      list.Add(Load(reader));
    }
    return list;
  }

  protected abstract T DoLoad(int id, DbDataReader reader);

  protected abstract string InsertQuery();

  public int Insert(T subject)
  {
    DbCommand command = null;
    try
    {
      command = DB.CreateCommand(InsertQuery());
      subject.Id = GetNextDatabaseId();
      command.Parameters.Add(DB.CreateParameter("@id", subject.Id));
      DoInsert(subject, command);
      command.ExecuteNonQuery();

      loadedMap.Add(subject.Id, subject);      
      return subject.Id;
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to insert", ex);
    }
    finally
    {
      DB.Dispose(command);
    }
  }

  protected abstract void DoInsert(T subject, DbCommand command);

  protected int GetNextDatabaseId()
  {
    // No implementation for example simplicity
    throw new NotImplementedException();
  }
}