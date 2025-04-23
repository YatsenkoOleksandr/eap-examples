using QueryObject.Domain;
using System.Reflection;

namespace QueryObject.MetadataMapping
{
    /// <summary>
    /// Corresponds to mapping of one field to one table column.
    /// Used generic type as reference to domain object class
    /// </summary>
    public class ColumnMap<T> where T : DomainObject
    {
        public string ColumnName { get; }

        public string FieldName { get; }

        private FieldInfo? _field;

        private DataMap<T>? _dataMap;

        public ColumnMap(string columnName, string fieldName, DataMap<T> dataMap)
        {
            ColumnName = columnName;
            FieldName = fieldName;
            _dataMap = dataMap;
            InitField();
        }

        private void InitField()
        {
            try
            {
                _field = typeof(T).GetField(FieldName);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"Unable to set up field: {FieldName}", ex);
                }
            }
        }

        public void SetField(object result, object value)
        {
            try
            {
                _field.SetValue(result, value);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in setting value for {FieldName}", ex);
            }
        }

        public object? GetValue(object subject)
        {
            return _field.GetValue(subject);
        }
    }
}
