using Repository.Domain;

namespace Repository.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        public IEnumerable<Person> DependentsOf(Person person);
    }
}
