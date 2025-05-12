using Repository.Domain;
using Repository.QueryObject;

namespace Repository.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : DomainObject
    {
        private readonly List<T> _inMemoryList = [];

        public T Add(T entity)
        {
            var existingDomainObject = FindByIdOrDefault(entity.Id);

            if (existingDomainObject is not null)
            {
                throw new InvalidOperationException("Domain object already exists.");
            }

            // entity.Id = GenerateNextId();
            _inMemoryList.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            var domainObjectToDelete = FindByIdOrDefault(entity.Id);
            if (domainObjectToDelete is not null)
            {
                _inMemoryList.Remove(domainObjectToDelete);
            }
        }

        public T? Get(int key)
        {
            return FindByIdOrDefault(key);
        }

        public IEnumerable<T> Matching(QueryObject<T> queryObject)
        {
            throw new NotImplementedException();
        }

        public T Update(T entity)
        {
            for (int i = 0; i < _inMemoryList.Count; i++)
            {
                if (_inMemoryList[i].Id == entity.Id)
                {
                    _inMemoryList[i] = entity;
                    return entity;
                }
            }

            throw new InvalidOperationException("Domain object not found.");
        }

        private T? FindByIdOrDefault(int id)
        {
            return _inMemoryList.FirstOrDefault(x => x.Id == id);
        }
    }
}
