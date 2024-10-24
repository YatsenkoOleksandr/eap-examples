namespace LazyLoad.Ghost;

/// <summary>
/// Employee domain class
/// </summary>
public class Employee : DomainObject
{
    private string name;

    public Employee(int key) : base(key)
    {
    }

    /// <summary>
    /// Field accessor triggers a load if the object is a ghost
    /// </summary>
    public string Name
    {
        get
        {
            Load();
            return name;
        }
        set
        {
            Load();
            name = value;
        }
    }
}