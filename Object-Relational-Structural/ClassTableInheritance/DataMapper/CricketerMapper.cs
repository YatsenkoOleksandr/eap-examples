using ClassTableInheritance.Domain;
using System.Data;

namespace ClassTableInheritance.DataMapper
{
    public class CricketerMapper : AbstractPlayerMapper
    {
        public const string TYPE_CODE = "C";

        public CricketerMapper(Gateway gateway) : base(gateway)
        {
        }

        public override string TypeCode => "C";

        /// <summary>
        /// Specific table for <see cref="Cricketer"/>
        /// </summary>
        protected override string TableName => "Cricketers";

        public Cricketer? Find(int id)
        {
            return AbstractFind(id, TableName) as Cricketer;
        }

        protected override DomainObject CreateDomainObject()
        {
            return new Cricketer();
        }

        /// <summary>
        /// Load <see cref="Cricketer"/> fields
        /// </summary>
        /// <param name="obj"></param>
        protected override void Load(DomainObject obj)
        {
            // Load superclass fields from superclass table
            base.Load(obj);

            // Query the Cricketer table
            DataRow? row = FindRow(obj.Id, TableName);

            Cricketer cricketer = obj as Cricketer;
            cricketer.BattingAverage = (double)row["battingAverage"];
        }

        /// <summary>
        /// Save <see cref="Cricketer"/> into a database
        /// </summary>
        /// <param name="obj"></param>
        protected override void Save(DomainObject obj)
        {
            // Save superclass fields
            base.Save(obj);

            DataRow? row = FindRow(obj.Id, TableName);

            Cricketer cricketer = obj as Cricketer;
            row["battingAverage"] = cricketer.BattingAverage;
        }

        protected override void AddRow(DomainObject domainObject)
        {
            // Insert row into superclass table
            base.AddRow(domainObject);

            // Insert row into specific table
            InsertRow(domainObject, TableFor(TableName));
        }

        public override void Delete(DomainObject obj)
        {
            // Delete row in superclass table
            base.Delete(obj);

            // Delete row in specific table
            DataRow? row = FindRow(obj.Id, TableName);
            row.Delete();
        }
    }
}
