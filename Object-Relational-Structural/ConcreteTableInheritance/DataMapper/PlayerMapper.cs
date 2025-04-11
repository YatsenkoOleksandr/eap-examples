using ConcreteTableInheritance.Domain;
using System.Data;

namespace ConcreteTableInheritance.DataMapper
{
    public class PlayerMapper : Mapper
    {
        private readonly CricketerMapper _cricketerMapper;

        public PlayerMapper(Gateway gateway): base(gateway)
        {
            _cricketerMapper = new CricketerMapper(gateway);
        }

        public override string TableName => throw new NotImplementedException();

        /// <summary>
        /// Method to find a <see cref="Player" /> by identifier.
        /// Method uses each concrete mapper to find an object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Player? Find(int id)
        {
            Player? player = null;

            player = _cricketerMapper.Find(id);
            if (player is not null)
            {
                return player;
            }
            /// Continue search by using other concrete mappers
            /// 
            /// player = _footballerMapper.Find(id);
            /// if (player is not null)
            /// {
            ///     return player;
            /// }
            ///

            return null;
        }

        /// <summary>
        /// Method to update <see cref="Player"/>.
        /// It searches for the correct concrete mapper and then delegates the call.
        /// </summary>
        /// <param name="obj"></param>
        public override void Update(DomainObject obj)
        {
            MapperFor(obj).Update(obj);
        }

        private Mapper MapperFor(DomainObject obj)
        {
            if (obj is Cricketer)
            {
                return _cricketerMapper;
            }

            throw new Exception("No mapper available");
        }

        /// <summary>
        /// Method to insert <see cref="Player"/>.
        /// It searches for the correct concrete mapper and then delegates the call.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int Insert(DomainObject obj)
        {
            return MapperFor(obj).Insert(obj);
        }

        /// <summary>
        /// Method to delete <see cref="Player"/>.
        /// It searches for the correct concrete mapper and then delegates the call.
        /// </summary>
        /// <param name="obj"></param>
        public override void Delete(DomainObject obj)
        {
            MapperFor(obj).Delete(obj);
        }

        /// <summary>
        /// Method not implemented since it is implemented in the concrete mappers
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override DomainObject CreateDomainObject()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Method not implemented since it is implemented in the concrete mappers
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="row"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void Save(DomainObject obj, DataRow row)
        {
            throw new NotImplementedException();
        }
    }
}
