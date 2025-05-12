using Repository.Domain;
using System.Text;

namespace Repository.MetadataMapping
{
    /// <summary>
    /// Corresponds to the mapping of one class to one table.
    /// Used generic as reference to domain object class
    /// </summary>
    public class DataMap<T> where T : DomainObject
    {
        public string TableName { get; }

        public List<ColumnMap<T>> Columns { get; } = [];

        public DataMap(Type domainClass, string tableName)
        {
            TableName = tableName;
        }

        public void AddColumn(string columnName, string columnType, string fieldName)
        {
            Columns.Add(new ColumnMap<T>(columnName, fieldName, this));
        }

        public string GetColumnForField(string field)
        {
            foreach (ColumnMap<T> column in Columns)
            {
                if (column.FieldName == field)
                {
                    return column.ColumnName;
                }
            }

            throw new Exception($"Unable to find column for {field}");
        }

        public string ColumnList
        {
            get
            {
                StringBuilder result = new("ID");
                foreach (ColumnMap<T> columnMap in Columns)
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
