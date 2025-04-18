using System.Text;

namespace MetadataMapping.Metadata
{
    /// <summary>
    /// Corresponds to the mapping of one class to one table.
    /// </summary>
    public class DataMap
    {
        public Type DomainClass { get; }

        public string TableName { get; }

        public List<ColumnMap> Columns { get; } = [];

        public DataMap(Type domainClass, string tableName)
        {
            DomainClass = domainClass;
            TableName = tableName;
        }

        public void AddColumn(string columnName, string columnType, string fieldName)
        {
            Columns.Add(new ColumnMap(columnName, fieldName, this));
        }

        public string ColumnList
        {
            get
            {
                StringBuilder result = new("ID");
                foreach (ColumnMap columnMap in Columns)
                {
                    result.Append(',');
                    result.Append(columnMap.ColumnName);
                }

                return result.ToString();
            }
        }

        public string UpdateList
        {
            get
            {
                StringBuilder result = new(" SET ");
                for (int i = 0; i < Columns.Count - 1; i++)
                {
                    result.Append(Columns[i].ColumnName);
                    result.Append("=?, ");
                }
                result.Append(Columns.Last().ColumnName);
                result.Append("=?");

                return result.ToString();
            }
        }

        public string InsertList
        {
            get
            {
                StringBuilder result = new();
                for (int i = 0; i < Columns.Count; i++)
                {
                    result.Append(", ");
                    result.Append("?");
                }
                result.Append(Columns.Last().ColumnName);
                result.Append("=?");

                return result.ToString();
            }
        }
    }
}
