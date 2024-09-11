using System.Data;

namespace TableDataGateway.ADO.NET.DATASET;

internal class PersonGateway: DataGateway
{
  public override string TableName
  {
    get { return "Person"; }
  }

  public long Insert(string lastName, string firstname, int numberOfDependents) {
    long key = GetNextID();
    DataRow newRow = Table.NewRow();
    newRow["id"] = key;
    newRow["lastName"] = lastName;
    newRow["firstName"] = firstname;
    newRow["numberOfDependents"] = numberOfDependents;
    Table.Rows.Add(newRow);
    return key;
  }

  private long GetNextID()
  {
    // No implementation for the example simplicity
    throw new NotImplementedException();
  }
}