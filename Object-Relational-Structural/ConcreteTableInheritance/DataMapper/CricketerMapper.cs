using ConcreteTableInheritance.Domain;
using System.Data;

namespace ConcreteTableInheritance.DataMapper
{
    public class CricketerMapper : AbstractPlayerMapper
    {
        public CricketerMapper(Gateway gateway) : base(gateway)
        {
        }

        public override string TableName => "Cricketers";

        public Cricketer? Find(int id)
        {
            return AbstractFind(id) as Cricketer;
        }

        protected override DomainObject CreateDomainObject()
        {
            return new Cricketer();
        }

        protected override void Load(DomainObject obj, DataRow row)
        {
            base.Load(obj, row);
            Cricketer? cricketer = obj as Cricketer;
            cricketer.BattingAverage = (double)row["battingAverage"];
        }

        protected override void Save(DomainObject obj, DataRow row)
        {
            base.Save(obj, row);
            Cricketer? cricketer = obj as Cricketer;
            row["battingAverage"] = cricketer.BattingAverage;
        }
    }
}
