using Repository.MetadataMapping;

namespace Repository.QueryObject
{
    /// <summary>
    /// Special-case criteria for pattern matching
    /// </summary>
    public class MatchCriteria : Criteria
    {
        public MatchCriteria(string field, object value) : base(string.Empty, field, value)
        {
        }

        /// <summary>
        /// Generates SQL predicate for pattern matching
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataMap"></param>
        /// <returns></returns>
        public override string GenerateSql<T>(DataMap<T>? dataMap)
        {
            return $"UPPER({dataMap.GetColumnForField(_field)}) LIKE UPPER('{_value}')";
        }
    }
}
