namespace LazyLoad.VirtualProxy;

/// <summary>
/// Domain class, plain C# object
/// </summary>
public class Supplier
{
    public int Id { get; set; }

    public IEnumerable<Product> Products { get; set; } = [];
}