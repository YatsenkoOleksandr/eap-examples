using ClassTableInheritance.Domain;
using System.Data;

namespace ClassTableInheritance.DataMapper
{
    public abstract class Mapper
    {
        protected Gateway _gateway;

        public Mapper(Gateway gateway)
        {
            _gateway = gateway;
        }

        protected DataTable TableFor(string name)
        {
            return _gateway.Data.Tables[name];
        }

        protected abstract DomainObject CreateDomainObject();

        protected abstract void Save(DomainObject obj);

        protected abstract void Load(DomainObject obj);

        protected DomainObject? AbstractFind(int id, string tableName)
        {
            DataRow? row = FindRow(id, TableFor(tableName));

            if (row == null)
            {
                return null;
            }

            DomainObject result = CreateDomainObject();
            result.Id = id;
            Load(result);
            return result;
        }

        protected DataRow? FindRow(int id, DataTable table)
        {
            string filter = $"id = {id}";
            DataRow[] results = table.Select(filter);

            if (results.Length == 0)
            {
                return null;
            }

            return results[0];
        }

        protected DataRow? FindRow(int id, string tableName)
        {
            return FindRow(id, TableFor(tableName));
        }

        /// <summary>
        /// Update entry point
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Update(DomainObject obj)
        {
            Save(obj);
        }

        /// <summary>
        /// Insert entry point
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns></returns>
        public virtual int Insert(DomainObject domainObject)
        {
            domainObject.Id = 1; // GetNextID()
            AddRow(domainObject);

            Save(domainObject);

            return domainObject.Id;
        }

        protected abstract void AddRow(DomainObject domainObject);

        protected virtual void InsertRow(DomainObject arg, DataTable table)
        {
            DataRow row = table.NewRow();
            row["id"] = arg.Id;
            table.Rows.Add(row);
        }

        /// <summary>
        /// Delete entry point
        /// </summary>
        /// <param name="obj"></param>
        public abstract void Delete(DomainObject obj);
    }
}
