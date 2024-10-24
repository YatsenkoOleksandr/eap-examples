namespace LazyLoad.Ghost;

/// <summary>
/// Registry which finds an appropriate data mapper for domain object via reflection
/// </summary>
public class MapperRegistry: IDataSource
{
    /// <summary>
    /// Mapper Registry by Domain Object type
    /// </summary>
    private static readonly IDictionary<Type, Mapper> mappers = new Dictionary<Type, Mapper>()
    {
        { typeof(Employee), new EmployeeMapper() }
    };

    private static Mapper GetMapper(Type type)
    {
        return mappers[type];
    }

    /// <summary>
    /// Loads domain object using appropriate Data Mapper
    /// </summary>
    /// <param name="obj"></param>
    public void Load(DomainObject obj)
    {
        GetMapper(obj.GetType()).Load(obj);
    }
}