using Repository.DataMapping;
using Repository.Domain;
using Repository.QueryObject;

namespace Repository.Repositories
{
    /// <summary>
    /// Repository which uses Relation Database as a data storage and uses <see cref="Mapper{T}"/> 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelationalDatabaseRepository<T> : IRepository<T> where T : DomainObject
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly Mapper<T> _dataMapper;

        public RelationalDatabaseRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dataMapper = unitOfWork.GetMapper<T>();
        }

        public T Add(T entity)
        {
            var idOfNewDomainObject = _dataMapper.Insert(entity);
            return _dataMapper.FindObject(idOfNewDomainObject);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T? Get(int key)
        {
            return _dataMapper.FindObject(key);
        }

        public IEnumerable<T> Matching(QueryObject<T> queryObject)
        {
            return queryObject.Execute(_unitOfWork);
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
