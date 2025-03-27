using ClassTableInheritance.Domain;
using System.Data;

namespace ClassTableInheritance.DataMapper
{
    public class PlayerMapper : Mapper
    {
        private readonly CricketerMapper _cricketerMapper;

        public PlayerMapper(Gateway gateway) : base(gateway)
        {
            _cricketerMapper = new CricketerMapper(gateway);
        }

        /// Still need table name for superclass
        protected string TableName => "Players";

        protected override DomainObject CreateDomainObject()
        {
            /// throw exception since creation is done in specific mapper
            throw new NotImplementedException();
        }

        protected override void Load(DomainObject obj)
        {
            /// throw exception since setting the field values is done in specific mapper
            throw new NotImplementedException();
        }

        protected override void Save(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        protected override void AddRow(DomainObject domainObject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load method which reads type code and determines which concrete mapper to use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Player? Find(int id)
        {
            // 1-st read from superclass table
            DataRow? row = FindRow(id, TableName);
            if (row == null)
            {
                return null;
            }

            string typeCode = (string)row["type"];

            switch (typeCode)
            {
                case CricketerMapper.TYPE_CODE:
                    // read from specific table
                    return _cricketerMapper.Find(id);

                default:
                    throw new Exception($"Unknown type '{typeCode}'");
            }
        }

        /// <summary>
        /// Update <see cref="Player"/> in the database
        /// </summary>
        /// <param name="obj"></param>
        public override void Update(DomainObject obj)
        {
            // Forward to appropriate specific mapper and update
            MapperFor(obj as Player).Update(obj);
        }

        /// <summary>
        /// Insert <see cref="Player" /> into the database
        /// </summary>
        /// <param name="domainObject"></param>
        /// <returns></returns>
        public override int Insert(DomainObject domainObject)
        {
            // Forward to appropriate specific mapper and insert
            return MapperFor(domainObject as Player).Insert(domainObject);
        }

        /// <summary>
        /// Delete <see cref="Player" /> from database
        /// </summary>
        /// <param name="domainObject"></param>
        public override void Delete(DomainObject domainObject)
        {
            // Forward to appropriate specific mapper and delete
            MapperFor(domainObject as Player).Delete(domainObject);
        }

        private Mapper MapperFor(Player obj)
        {
            if (obj is Cricketer)
            {
                return _cricketerMapper;
            }

            throw new Exception($"No mapper available for type: '{obj.GetType().FullName}'");
        }
    }
}
