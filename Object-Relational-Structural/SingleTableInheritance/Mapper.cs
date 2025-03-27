using SingleTableInheritance.Domain;
using System.Data;

namespace SingleTableInheritance
{
    public abstract class Mapper
    {
        public Mapper(Gateway gateway)
        {
            Gateway = gateway;
        }

        protected DataTable Table
        {
            get
            {
                return Gateway.Data.Tables[TableName];
            }
        }

        protected Gateway Gateway;

        abstract protected string TableName { get; }

        protected abstract DomainObject CreateDomainObject();

        protected abstract void Save(DomainObject obj, DataRow row);

        protected DomainObject? AbstractFind(int id)
        {
            DataRow? row = FindRow(id);

            if (row == null)
            {
                return null;
            }

            return Find(row);
        }

        protected DataRow? FindRow(int id)
        {
            string filter = $"id = {id}";
            DataRow[] results = Table.Select(filter);

            if (results.Length == 0)
            {
                return null;
            }

            return results[0];
        }

        public DomainObject? Find(DataRow row)
        {
            DomainObject result = CreateDomainObject();
            Load(result, row);
            return result;
        }

        public virtual void Update(DomainObject obj)
        {
            Save(obj, FindRow(obj.Id));
        }

        public virtual int Insert(DomainObject domainObject)
        {
            DataRow row = Table.NewRow();
            domainObject.Id = 1; // GetNextID()
            row["id"] = domainObject.Id;

            Save(domainObject, row);
            Table.Rows.Add(row);

            return domainObject.Id;
        }

        public virtual void Delete(DomainObject domainObject)
        {
            DataRow row = FindRow(domainObject.Id);
            row.Delete();
        }

        protected virtual void Load(DomainObject obj, DataRow row)
        {
            obj.Id = (int)row["id"];
        }
    }
}
