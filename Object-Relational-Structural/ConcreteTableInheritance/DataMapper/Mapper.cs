using ConcreteTableInheritance.Domain;
using System.Collections;
using System.Data;

namespace ConcreteTableInheritance.DataMapper
{
    public abstract class Mapper
    {
        private readonly IDictionary _identityMap = new Hashtable();

        protected readonly Gateway gateway;

        public abstract string TableName { get; }

        protected DataTable? Table => gateway.Data.Tables[TableName];

        public Mapper(Gateway gateway)
        {
            this.gateway = gateway;
        }

        public DomainObject? AbstractFind(int id)
        {
            DataRow? row = FindRow(id);
            if (row is null)
            {
                return null;
            }

            DomainObject result = CreateDomainObject();
            Load(result, row);
            return result;
        }

        private DataRow? FindRow(int id)
        {
            string filter = $"id = {id}";
            DataRow[]? results = Table?.Select(filter);

            if (results is null || results.Length == 0)
            {
                return null;
            }

            return results[0];
        }

        protected abstract DomainObject CreateDomainObject();

        protected virtual void Load(DomainObject obj, DataRow row)
        {
            obj.Id = (int)row["id"];
        }

        public virtual void Update(DomainObject obj)
        {
            Save(obj, FindRow(obj.Id));
        }

        protected abstract void Save(DomainObject obj, DataRow row);

        public virtual int Insert(DomainObject obj)
        {
            DataRow row = Table.NewRow();
            // obj.Id = GetNextID();
            row["id"] = obj.Id;
            Save(obj, row);
            Table.Rows.Add(row);
            return obj.Id;
        }

        public virtual void Delete(DomainObject obj)
        {
            DataRow? row = FindRow(obj.Id);
            row?.Delete();
        }
    }
}
