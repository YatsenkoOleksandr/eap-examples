using SingleTableInheritance.Domain;
using System.Data;

namespace SingleTableInheritance.DataMapper
{
    public class PlayerMapper : Mapper
    {
        private readonly CricketerMapper _cricketerMapper;

        public PlayerMapper(Gateway gateway) : base(gateway)
        {
            _cricketerMapper = new CricketerMapper(gateway);
        }

        /// throw exceptions since the base <see cref="Player" /> is an abstract 
        protected override string TableName => throw new NotImplementedException();

        protected override DomainObject CreateDomainObject()
        {
            /// throw exceptions since the base <see cref="Player" /> is an abstract 
            throw new NotImplementedException();
        }

        protected override void Save(DomainObject obj, DataRow row)
        {
            /// throw exceptions since the base <see cref="Player" /> is an abstract 
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
            DataRow row = FindRow(id);
            if (row == null)
            {
                return null;
            }

            string typeCode = (string)row["type"];

            switch (typeCode)
            {
                case CricketerMapper.TYPE_CODE:
                    return _cricketerMapper.Find(row) as Player;

                default:
                    throw new Exception($"Unknown type '{typeCode}'");
            }
        }

        public override void Update(DomainObject obj)
        {
            // Forward to appropriate specific mapper and update
            MapperFor(obj as Player).Update(obj);
        }

        public override int Insert(DomainObject domainObject)
        {
            // Forward to appropriate specific mapper and insert
            return MapperFor(domainObject as Player).Insert(domainObject);
        }

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
