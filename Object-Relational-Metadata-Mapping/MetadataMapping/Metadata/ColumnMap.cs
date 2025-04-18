using System.Reflection;

namespace MetadataMapping.Metadata
{
    /// <summary>
    /// Corresponds to mapping of one field to one table column.
    /// </summary>
    public class ColumnMap
    {
        public string ColumnName { get; }

        public string FieldName { get; }

        private FieldInfo? _field;

        private DataMap? _dataMap;

        public ColumnMap(string columnName, string fieldName, DataMap dataMap)
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
                _field = _dataMap?.DomainClass.GetField(FieldName);
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
