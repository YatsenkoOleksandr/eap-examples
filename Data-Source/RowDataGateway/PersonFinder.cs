using System.Data;
using System.Data.Common;

namespace RowDataGateway;

internal class PersonFinder
{
  public PersonGateway Find(long id)
  {
    var existingPerson = Registry.GetPersonOrDefault(id);
    if (existingPerson != null)
    {
      return existingPerson;
    }

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

      var person = PersonGateway.Load(reader);
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

  public IEnumerable<PersonGateway> FindResponsibles()
  {
    var responsibles = new List<PersonGateway>();

    var query = @"
      SELECT id, lastname, firstname, number_of_dependents
      FROM People
      WHERE number_of_dependents > 0;";

    DbCommand command = null;
    IDataReader reader = null;
    try
    {
      command = DB.CreateCommand(query);
      reader = command.ExecuteReader();

      while (reader.Read())
      {
        responsibles.Add(PersonGateway.Load(reader));
      }
      return responsibles;
    }
    catch (Exception ex)
    {
      throw new ApplicationException("Failed to get responsibles", ex);
    }
    finally
    {
      DB.Dispose(command, reader);
    }
  }
}