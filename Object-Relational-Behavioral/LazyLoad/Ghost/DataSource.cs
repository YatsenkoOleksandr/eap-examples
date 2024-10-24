namespace LazyLoad.Ghost;

public class DataSource
{
    private static readonly IDataSource dataSource;

    /// <summary>
    /// Entry point to load a <see cref="DomainObject"/>
    /// </summary>
    /// <param name="obj">domain class to load</param>
    public static void Load(DomainObject obj)
    {
        dataSource.Load(obj);
    }
}