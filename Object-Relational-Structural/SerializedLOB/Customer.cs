using System.Data;
using System.Xml.Serialization;

namespace SerializedLOB
{
    /// <summary>
    /// Active Record with Serialized LOB
    /// </summary>
    public class Customer
    {
        public int ID { get; set; }

        public string? Name { get; set; }

        /// <summary>
        /// Serialized LOB
        /// </summary>
        public List<Department>? Departments { get; set; }

        public void Insert()
        {
            const string insertQuery = @"
                INSERT INTO customers
                VALUES (@id, @name, @departments)
            ";

            var id = ID;
            var name = Name;
            var departments = SerializeDepartments();

            // Execute insert
        }

        public static Customer Load(IDataReader dataReader)
        {
            var id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
            var name = dataReader.GetString(dataReader.GetOrdinal("name"));
            var departments = dataReader.GetString(dataReader.GetOrdinal("departments"));

            return new Customer()
            {
                ID = id,
                Name = name,
                Departments = DeserializeDepartments(departments),
            };
        }

        private string? SerializeDepartments()
        {
            if (Departments == null)
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(List<Department>));
            using (var textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, Departments);
                return textWriter.ToString();
            }            
        }

        private static List<Department>? DeserializeDepartments(string? serialized)
        {
            if (string.IsNullOrEmpty(serialized))
            {
                return null;
            }

            var serializer = new XmlSerializer(typeof(List<Department>));
            using (var textReader = new StringReader(serialized))
            {
                return (List<Department>) serializer.Deserialize(textReader); 
            }
        }
    }
}
