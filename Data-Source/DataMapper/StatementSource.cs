using System.Data.Common;

namespace DataMapper;

internal interface StatementSource
{
  string Sql();

  IEnumerable<DbParameter> Parameters();
}