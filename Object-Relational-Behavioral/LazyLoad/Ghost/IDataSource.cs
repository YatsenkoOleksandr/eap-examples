namespace LazyLoad.Ghost;

/// <summary>
/// Interface to load a domain object
/// </summary>
public interface IDataSource
{
    void Load(DomainObject obj);
}