using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace RowDataGateway;

internal class PersonGateway
{
  /*
    Data fields
  */
  public long Id { get; private set; } = 0;

  public string LastName { get; set; } = string.Empty;

  public string FirstName { get; set; } = string.Empty;

  public int NumberOfDependents { get; set; } = 0;

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
      // Use Registry to hold Identity Maps
      Registry.AddPerson(this);
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

  public static PersonGateway Load(IDataRecord record)
  {
    long id = record.GetInt64(record.GetOrdinal("id"));
    var existingPerson = Registry.GetPersonOrDefault(id);
    if (existingPerson != null)
    {
      return existingPerson;
    }

    string lastName = record.GetString(record.GetOrdinal("lastname"));
    string firstName = record.GetString(record.GetOrdinal("firstname"));
    int numberOfDependents = record.GetInt32(record.GetOrdinal("number_of_dependents"));

    var person = new PersonGateway()
    {
      Id = id,
      LastName = lastName,
      FirstName = firstName,
      NumberOfDependents = numberOfDependents,
    };
    
    // Use Registry to hold Identity Map
    Registry.AddPerson(person);
    return person;
  }

  private long GetNextDatabaseId()
  {
    // No implementation for example simplicity
    throw new NotImplementedException();
  }
}
