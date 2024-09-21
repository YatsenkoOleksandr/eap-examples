using System.Data.Common;

namespace DataMapper;

internal class PersonMapper : AbstractMapper<Person>
{
  protected override string FindQuery()
  {
    return @"
      SELECT id, lastname, firstname, number_of_dependents
      FROM People
      WHERE id = @id;
    ";
  }

  public Person Find(int id)
  {
    return AbstractFind(id);
  }

  public IEnumerable<Person> FindByLastName(string lastName)
  {
    var query = @"
      SELECT id, lastname, firstname, number_of_dependents
      FROM People
      WHERE UPPER(lastname) LIKE UPPER(@lastname)
      ORDER BY lastname;
    ";

    DbCommand command = null;
    DbDataReader reader = null;
    try
    {
      command = DB.CreateCommand(query);
      command.Parameters.Add(DB.CreateParameter("@lastname", lastName));
      reader = command.ExecuteReader();
      return LoadAll(reader);
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to find persons by last name '{lastName}'.", ex);
    }
    finally
    {
      DB.Dispose(command, reader);
    }
  }

  public IEnumerable<Person> FindByLastName2(string lastName)
  {
    var statement = new FindByLastNameStatement(lastName);
    return FindMany(statement);
  }

  public void Update(Person person)
  {
    var query = @"
      UPDATE People
      SET
        lastname = @lastname,
        firstname = @firstname,
        number_of_dependents = @numberOfDependents
      WHERE id = @id;
    ";

    DbCommand command = null;
    try
    {
      command = DB.CreateCommand(query);
      command.Parameters.Add(DB.CreateParameter("@lastname", person.LastName));
      command.Parameters.Add(DB.CreateParameter("@firstname", person.FirstName));
      command.Parameters.Add(DB.CreateParameter("@numberOfDependents", person.NumberOfDependents));
      command.Parameters.Add(DB.CreateParameter("@id", person.Id));
      command.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
      throw new ApplicationException($"Failed to update person #{person.Id}", ex);
    }
    finally
    {
      DB.Dispose(command);
    }
  }

  protected override Person DoLoad(int id, DbDataReader reader)
  {
    var lastName = reader.GetString(reader.GetOrdinal("lastname"));
    var firstName = reader.GetString(reader.GetOrdinal("firstname"));
    var numberOfDependents = reader.GetInt32(reader.GetOrdinal("number_of_dependents"));

    return new Person
    {
      Id = id,
      FirstName = firstName,
      LastName = lastName,
      NumberOfDependents = numberOfDependents,
    };
  }

  protected override string InsertQuery()
  {
    return @"
      INSERT INTO People VALUES (@id, @lastname, @firstname, @number_of_dependents)
    ";
  }

  protected override void DoInsert(Person subject, DbCommand command)
  {
    command.Parameters.Add(DB.CreateParameter("@lastname", subject.LastName));
    command.Parameters.Add(DB.CreateParameter("@firstname", subject.FirstName));
    command.Parameters.Add(DB.CreateParameter("@numberOfDependents", subject.NumberOfDependents));
  }

  internal class FindByLastNameStatement : StatementSource
  {
    private readonly string lastName;

    public FindByLastNameStatement(string lastName)
    {
      this.lastName = lastName;
    }

    public string Sql()
    {
      return @"
        SELECT id, lastname, firstname, number_of_dependents
        FROM People
        WHERE UPPER(lastname) LIKE UPPER(@lastname)
        ORDER BY lastname;
      ";
    }

    public IEnumerable<DbParameter> Parameters()
    {
      return
      [
        DB.CreateParameter("@lastname", lastName)
      ];
    }
  }
}