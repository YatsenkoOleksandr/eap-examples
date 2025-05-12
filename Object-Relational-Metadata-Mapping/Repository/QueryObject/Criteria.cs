using Repository.Domain;
using Repository.MetadataMapping;

namespace Repository.QueryObject
{
    public class Criteria
    {
        private readonly string _sqlOperator;

        protected readonly string _field;

        protected readonly object _value;

        protected Criteria(string sql, string field, object value)
        {
            _sqlOperator = sql;
            _field = field;
            _value = value;
        }

        public static Criteria GreaterThan(string fieldName, int value)
        {
            return GreaterThan(fieldName, value as object);
        }

        public static Criteria GreaterThan(string fieldName, object value)
        {
            return new Criteria(" > ", fieldName, value);
        }

        public static Criteria Matches(string fieldName, string pattern)
        {
            return new MatchCriteria(fieldName, pattern);
        }

        /// <summary>
        /// Generates SQL predicate inside WHERE clause
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataMap"></param>
        /// <returns></returns>
        public virtual string GenerateSql<T>(DataMap<T>? dataMap) where T : DomainObject
        {
            return $"{dataMap.GetColumnForField(_field)} {_sqlOperator} {_value}";
        }
    }
}
