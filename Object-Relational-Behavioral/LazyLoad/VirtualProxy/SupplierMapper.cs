namespace LazyLoad.VirtualProxy;

/// <summary>
/// Data Mapper for <see cref="Supplier"/>
/// </summary>
public class SupplierMapper
{
    /// <summary>
    /// Loads <see cref="Supplier"/>
    /// </summary>
    /// <remarks>
    /// Does not load products, but provides virtual proxy to load products
    /// </remarks>
    /// <returns></returns>
    protected Supplier Load()
    {
        ProductLoader productLoader = new();
        VirtualList<Product> virtualProducts = new(productLoader);

        return new Supplier()
        {
            Id = 86,

            // Provide Virtual Proxy (VirtualList) instead of common List/Array
            // Domain class isn't even aware that `Products` is a Lazy Load
            Products = virtualProducts,
        };
    }
}