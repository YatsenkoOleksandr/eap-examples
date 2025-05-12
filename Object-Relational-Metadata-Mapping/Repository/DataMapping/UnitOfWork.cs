using Repository.Domain;

namespace Repository.DataMapping
{
    public class UnitOfWork
    {
        public Mapper<T> GetMapper<T>() where T : DomainObject
        {
            Type domainType = typeof(T);
            if (domainType == typeof(Person))
            {
                return new PersonMapper() as Mapper<T>;
            }

            throw new NotImplementedException();
        }
    }
}
