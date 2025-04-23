using QueryObject.Domain;
using QueryObject.MetadataMapping;
using System.Data.Common;

namespace QueryObject.DataMapping
{
    public abstract class Mapper<T> where T : DomainObject
    {
        public DataMap<T>? DataMap { get; protected set; }

        public T FindObject(int key)
        {
            // if (uow.isLoaded(key)) return uow.getObject(key);

            string sql = @$"SELECT {DataMap.ColumnList}
                            FROM {DataMap.TableName}
                            WHERE ID = @id;";

            DbCommand? command = null;
            T? result = null;
            DbDataReader? reader = null;
            try
            {
                command = DB.CreateCommand(sql);
                DbParameter param = DB.CreateParameter("@id", key);
                command.Parameters.Add(param);
                reader = command.ExecuteReader();
                reader.Read();

                result = Load(reader);
                return result;

            }
            finally
            {
                DB.Dispose(command, reader);
            }
        }

        public T Load(DbDataReader reader)
        {
            int key = (int)reader["ID"];

            // if (uow.isLoaded(key)) return uow.getObject(key);

            T result = Activator.CreateInstance<T>();
            result.Id = key;
            LoadFields(reader, result);

            return result;
        }

        /// <summary>
        /// Method to set field values into domain object from database.
        /// Marked as virtual to have an ability to override it in subclass mapper for more complicated cases.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="result"></param>
        protected virtual void LoadFields(DbDataReader reader, T? result)
        {
            foreach (var columnMap in DataMap.Columns)
            {
                object columnValue = reader[columnMap.ColumnName];
                columnMap.SetField(result, columnValue);
            }
        }

        public void Update(T obj)
        {
            string sql = $@"UPDATE {DataMap.TableName}
                            {DataMap.UpdateList}
                            WHERE ID = ?";

            DbCommand? command = null;
            try
            {
                command = DB.CreateCommand(sql);
                foreach (var columnMap in DataMap.Columns)
                {
                    DbParameter param = DB.CreateParameter(columnMap.GetValue(obj));
                    command.Parameters.Add(param);
                }

                command.Parameters.Add(DB.CreateParameter(obj.Id));
                command.ExecuteNonQuery();
            }
            finally
            {
                DB.Dispose(command);
            }
        }

        public int Insert(T obj)
        {
            string sql = $@"INSERT INTO {DataMap.TableName}
                            VALUES (?, {DataMap.InsertList});";

            DbCommand? command = null;
            try
            {
                command = DB.CreateCommand(sql);
                command.Parameters.Add(DB.CreateParameter(obj.Id));
                foreach (var columnMap in DataMap.Columns)
                {
                    DbParameter param = DB.CreateParameter(columnMap.GetValue(obj));
                    command.Parameters.Add(param);
                }
                command.ExecuteNonQuery();
            }
            finally
            {
                DB.Dispose(command);
            }

            return obj.Id;
        }

        /// <summary>
        /// Generic finder method to query multiple objects by where-clause
        /// </summary>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        public HashSet<T> FindObjectsWhere(string whereClause)
        {
            string sql = $@"SELECT {DataMap.ColumnList}
                            FROM {DataMap.TableName}
                            WHERE {whereClause};";

            DbCommand? command = null;
            DbDataReader? reader = null;
            try
            {
                command = DB.CreateCommand(sql);
                reader = command.ExecuteReader();

                return LoadAll(reader);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                DB.Dispose(command, reader);
            }
        }

        protected HashSet<T> LoadAll(DbDataReader reader)
        {
            HashSet<T> result = [];

            while (reader.Read())
            {
                T newObject = Load(reader);
                result.Add(newObject);
            }

            return result;
        }
    }
}
