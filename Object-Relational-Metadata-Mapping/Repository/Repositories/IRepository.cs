using Repository.Domain;
using Repository.QueryObject;

namespace Repository.Repositories
{
    /// <summary>
    /// Repository interface to mediate between domain and data mapping layers like a collection.
    /// </summary>
    /// <typeparam name="T">Domain Object class</typeparam>
    public interface IRepository<T> where T : DomainObject
    {
        public T Add(T entity);

        public T Update(T entity);

        public void Delete(T entity);

        public T? Get(int key);

        public IEnumerable<T> Matching(QueryObject<T> queryObject);
    }
}
