
namespace LazyLoad.VirtualProxy;

/// <summary>
/// Loader for <see cref="Product"/>
/// </summary>
public class ProductLoader : IVirtualListLoader<Product>
{
    /// <summary>
    /// Simulates loading <see cref="Product"/> from external resource
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> Load()
    {
        /*
            return ProductMapper.create().findForSupplier(id);

            return in-memory products instead of call to external resource
        */
        return [new Product() { Id = 10, }];
    }
}