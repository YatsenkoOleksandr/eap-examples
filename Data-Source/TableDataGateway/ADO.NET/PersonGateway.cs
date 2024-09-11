using System.Data;
using System.Data.OleDb;

namespace TableDataGateway.ADO.NET;

internal class PersonGateway
{
  private readonly OleDbConnection dbConnection;

  public PersonGateway(OleDbConnection dbConnection)
  {
    this.dbConnection = dbConnection;
  }

  /**
    Returning ADO.NET's data reader to access the returned data
  */

  public IDataReader FindAll()
  {
    string sql = "select * from person";
    return new OleDbCommand(sql, dbConnection).ExecuteReader();
  }

  public IDataReader FindWithLastName(string lastName)
  {
    string sql = "SELECT * FROM person WHERE lastname = ?";
    IDbCommand comm = new OleDbCommand(sql, dbConnection);
    comm.Parameters.Add(new OleDbParameter("lastname", lastName));
    return comm.ExecuteReader();
  }

  public IDataReader FindWhere(string whereClause)
  {
    string sql = string.Format("select * from person where {0}", whereClause);
    return new OleDbCommand(sql, dbConnection).ExecuteReader();
  }

  /**
    Get hold of an individual row of data
  */

  public Object[] FindRow(long key)
  {
    string sql = "SELECT * FROM person WHERE id = ?";
    IDbCommand comm = new OleDbCommand(sql, dbConnection);
    comm.Parameters.Add(new OleDbParameter("key",key));
    IDataReader reader = comm.ExecuteReader();
    reader.Read();
    object[] result = new object[reader.FieldCount];
    reader.GetValues(result);
    reader.Close();
    return result;
  }

  public void Update(long key, string lastname, string firstname, long numberOfDependents)
  {
    string sql = @"
      UPDATE person
      SET lastname = ?, firstname = ?, numberOfDependents = ?
      WHERE id = ?";
    
    IDbCommand comm = new OleDbCommand(sql, dbConnection);
    comm.Parameters.Add(new OleDbParameter ("last", lastname));
    comm.Parameters.Add(new OleDbParameter ("first", firstname));
    comm.Parameters.Add(new OleDbParameter ("numDep", numberOfDependents));
    comm.Parameters.Add(new OleDbParameter ("key", key));
    comm.ExecuteNonQuery();
  }

  public long Insert(string lastname, string firstname, long numberOfDependents)
  {
    string sql = "INSERT INTO person VALUES (?,?,?,?)";
    long key = GetNextID();
    IDbCommand comm = new OleDbCommand(sql, dbConnection);
    comm.Parameters.Add(new OleDbParameter ("key", key));
    comm.Parameters.Add(new OleDbParameter ("last", lastname));
    comm.Parameters.Add(new OleDbParameter ("first", firstname));
    comm.Parameters.Add(new OleDbParameter ("numDep", numberOfDependents));
    comm.ExecuteNonQuery();
    return key;
  }

  public void Delete(long key)
  {
    string sql = "DELETE FROM person WHERE id = ?";
    IDbCommand comm = new OleDbCommand(sql, dbConnection);
    comm.Parameters.Add(new OleDbParameter ("key", key));
    comm.ExecuteNonQuery();
  }

  protected long GetNextID()
  {
    // throw an error for example simplicity
    throw new NotImplementedException();
  }
}
