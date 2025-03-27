using ClassTableInheritance.Domain;
using System.Data;

namespace ClassTableInheritance.DataMapper
{
    public abstract class AbstractPlayerMapper : Mapper
    {
        protected AbstractPlayerMapper(Gateway gateway) : base(gateway)
        {
        }

        /// <summary>
        /// The superclass table
        /// </summary>
        protected virtual string TableName => "Players";

        /// <summary>
        /// Type code to determine which class to use during data read.
        /// Implemented in the subclasses.
        /// </summary>
        public abstract string TypeCode { get; }

        /// <summary>
        /// Load superclass fields from superclass table
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Load(DomainObject obj)
        {
            DataRow? row = FindRow(obj.Id, TableName);

            Player player = obj as Player;
            player.Name = (string)row["name"];
        }

        /// <summary>
        /// Save of superclass fields and type code
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Save(DomainObject obj)
        {
            DataRow? row = FindRow(obj.Id, TableName);

            Player player = obj as Player;
            row["name"] = player.Name;
            row["type"] = TypeCode;
        }

        protected override void AddRow(DomainObject domainObject)
        {
            InsertRow(domainObject, TableFor(TableName));
        }

        public override void Delete(DomainObject obj)
        {
            DataRow? row = FindRow(obj.Id, TableFor(TableName));
            row.Delete();
        }
    }
}
