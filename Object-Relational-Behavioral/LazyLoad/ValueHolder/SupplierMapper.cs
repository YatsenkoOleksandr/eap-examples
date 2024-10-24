namespace LazyLoad.ValueHolder;

/// <summary>
/// Data Mapper for <see cref="Supplier"/>
/// </summary>
public class SupplierMapper
{
    protected Supplier Load()
    {
        // Initialize loader and value holder
        var productLoader = new ProductLoader();
        var productHolder = new ValueHolder<Product>(productLoader);

        var productLazy = new Lazy<Product>(productLoader.Load);

        return new Supplier()
        {
            Id = 89,

            // Provide value holder
            Product = productHolder,

            // Provide lazy
            LazyProducts = productLazy,
        };
    }
}