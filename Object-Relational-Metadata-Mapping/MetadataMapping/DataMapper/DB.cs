using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace MetadataMapping.DataMapper
{
    internal static class DB
    {
        private static SqlConnection dbConnection;

        public static DbCommand CreateCommand(string sqlQuery)
        {
            return new SqlCommand(sqlQuery, dbConnection);
        }

        public static DbParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        public static DbParameter CreateParameter(object value)
        {
            return new SqlParameter()
            {
                Value = value,
            };
        }

        public static void Dispose(params IDisposable?[] dbResources)
        {
            foreach (var dbResource in dbResources)
            {
                dbResource?.Dispose();
            }
        }
    }
}
