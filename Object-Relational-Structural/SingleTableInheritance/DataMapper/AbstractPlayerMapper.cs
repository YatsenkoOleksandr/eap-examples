using SingleTableInheritance.Domain;
using System.Data;

namespace SingleTableInheritance.DataMapper
{
    public abstract class AbstractPlayerMapper : Mapper
    {
        protected AbstractPlayerMapper(Gateway gateway) : base(gateway)
        {
        }

        /// <summary>
        /// The single table for the whole hierarchy, defined by abstract data mapper
        /// </summary>
        protected override string TableName => "Players";

        /// <summary>
        /// Type code to determine which class to use during data read.
        /// Implemented in the subclasses.
        /// </summary>
        public abstract string TypeCode { get; }

        /// <summary>
        /// Load of common information
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Load(DomainObject obj, DataRow row)
        {
            base.Load(obj, row);
            Player player = obj as Player;
            player.Name = (string)row["name"];
        }

        /// <summary>
        /// Save of common information and type code
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        protected override void Save(DomainObject obj, DataRow row)
        {
            Player player = obj as Player;
            row["name"] = player.Name;
            row["type"] = TypeCode;
        }
    }
}
