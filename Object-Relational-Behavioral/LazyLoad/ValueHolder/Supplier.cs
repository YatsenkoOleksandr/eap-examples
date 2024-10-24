namespace LazyLoad.ValueHolder;

/// <summary>
/// Domain class
/// </summary>
public class Supplier
{
    public int Id { get; set; }

    /// <summary>
    /// Value Holder of <see cref="Product"/>
    /// </summary>
    public ValueHolder<Product>? Product { get; set; }

    /// <summary>
    /// Built-in C# Value Holder
    /// </summary>
    public Lazy<Product>? LazyProducts { get; set; }
}