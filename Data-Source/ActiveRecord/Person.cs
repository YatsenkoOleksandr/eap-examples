using System.Data;
using System.Data.Common;

namespace ActiveRecord;

public class Person
{

#region Data Fields

  public long Id { get; private set; } = 0;

  public string LastName { get; set; } = string.Empty;

  public string FirstName { get; set; } = string.Empty;

  public int NumberOfDependents { get; set; } = 0;

#endregion

#region Data Access Methods

  public static Person Find(long id)
  {
    var findQuery = @"
      SELECT id, lastname, firstname, number_of_dependents
      FROM People
      WHERE id = @id;";

    DbCommand command = null;
    IDataReader reader = null;
    try
    {
      command = DB.CreateCommand(findQuery);
      command.Parameters.Add(DB.CreateParameter("@id", id));
      reader = command.ExecuteReader();
      reader.Read();

      var person = Load(reader);
      return person;
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to get person #{id}.", ex);
    }
    finally
    {
      DB.Dispose(command, reader);
    }
  }

  public static Person Load(IDataRecord record)
  {
    long id = record.GetInt64(record.GetOrdinal("id"));

    string lastName = record.GetString(record.GetOrdinal("lastname"));
    string firstName = record.GetString(record.GetOrdinal("firstname"));
    int numberOfDependents = record.GetInt32(record.GetOrdinal("number_of_dependents"));

    var person = new Person()
    {
      Id = id,
      LastName = lastName,
      FirstName = firstName,
      NumberOfDependents = numberOfDependents,
    };

    return person;
  }

  public long Insert()
  {
    const string insertQuery = @"INSERT INTO People VALUES (@id, @lastname, @firstname, @numberOfDependents)";
    DbCommand command = null;
    try
    {
      command = DB.CreateCommand(insertQuery);
      Id = GetNextDatabaseId();
      command.Parameters.Add(DB.CreateParameter("@id", Id));
      command.Parameters.Add(DB.CreateParameter("@lastname", LastName));
      command.Parameters.Add(DB.CreateParameter("@firstname", FirstName));
      command.Parameters.Add(DB.CreateParameter("@numberOfDependents", NumberOfDependents));
      command.ExecuteNonQuery();
      return Id;
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to insert person #{Id}", ex);
    }
    finally
    {
      DB.Dispose(command);
    }
  }

  public void Update()
  {
    const string updateQuery = @"
      UPDATE People
      SET
        lastname = @lastname,
        firstname = @firstname,
        number_of_dependents = @numberOfDependents
      WHERE id = @id;";

    DbCommand command = null;
    try
    {
      command = DB.CreateCommand(updateQuery);
      command.Parameters.Add(DB.CreateParameter("@lastname", LastName));
      command.Parameters.Add(DB.CreateParameter("@firstname", FirstName));
      command.Parameters.Add(DB.CreateParameter("@numberOfDependents", NumberOfDependents));
      command.Parameters.Add(DB.CreateParameter("@id", Id));
      command.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to update person #{Id}", ex);
    }
    finally
    {
      DB.Dispose(command);
    }
  }

  private long GetNextDatabaseId()
  {
    // No implementation for example simplicity
    throw new NotImplementedException();
  }

#endregion

#region Domain Logic

  public decimal GetExemption()
  {
    decimal baseExemption = 1500;

    decimal dependentExemption = 750;

    return baseExemption + dependentExemption * NumberOfDependents;
  }

#endregion
}
