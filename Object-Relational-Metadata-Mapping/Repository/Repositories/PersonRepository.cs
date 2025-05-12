using Repository.Domain;
using Repository.QueryObject;

namespace Repository.Repositories
{
    /// <summary>
    /// Repository for <see cref="Person"/> as a Decorator over another repository
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private IRepository<Person> _repository;

        public PersonRepository(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public Person? Get(int key)
        {
            return _repository.Get(key);
        }

        public Person Add(Person entity)
        {
            return _repository.Add(entity);
        }

        public void Delete(Person entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<Person> Matching(QueryObject<Person> queryObject)
        {
            return _repository.Matching(queryObject);
        }

        public Person Update(Person entity)
        {
            return _repository.Update(entity);
        }

        public IEnumerable<Person> DependentsOf(Person person)
        {
            var query = new QueryObject<Person>();

            // ... construct query object ...

            return Matching(query);
        }
    }
}
