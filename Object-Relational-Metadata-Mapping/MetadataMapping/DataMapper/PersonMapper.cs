using MetadataMapping.Domain;
using MetadataMapping.Metadata;
using System.Data.Common;

namespace MetadataMapping.DataMapper
{
    public class PersonMapper : Mapper<Person>
    {
        public PersonMapper()
        {
            LoadDataMap();
        }

        /// <summary>
        /// Method to initiate a Metadata Mapping.
        /// 
        /// Can be converted into hook method - declared as abstract in <see cref="Mapper{T}"/> and overriden in specific mapper.
        /// </summary>
        protected void LoadDataMap()
        {
            DataMap = new DataMap(typeof(Person), "people");
            DataMap.AddColumn("lastname", "varchar", "LastName");
            DataMap.AddColumn("firstname", "varchar", "FirstName");
            DataMap.AddColumn("number_of_dependents", "int", "NumberOfDependents");
        }

        /// <summary>
        /// Special case finder on the mapper subclass.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public HashSet<Person> FindLastNamesLike(string pattern)
        {
            string sql = $@"SELECT {DataMap.ColumnList}
                            FROM {DataMap.TableName}
                            WHERE UPPER(lastName) LIKE UPPER(?);";

            DbCommand? command = null;
            DbDataReader? reader = null;
            try
            {
                command = DB.CreateCommand(sql);
                command.Parameters.Add(DB.CreateParameter(pattern));
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
    }
}
