namespace LazyLoad.LazyInitialization;

/// <summary>
/// Domain class which knows how to load <see cref="Product"/>
/// </summary>
public class Supplier
{
    private IEnumerable<Product>? products = null;

    /// <summary>
    /// Method to access products
    /// </summary>
    /// <remarks>
    /// Method loads products from the database on the first access
    /// </remarks>
    /// <returns></returns>
    public IEnumerable<Product> GetProducts()
    {
        if (products is null)
        {
            /*
                products = Product.FindForSupplier();

                use in-memory array instead of call to extrernal resource
            */
            products = [];
        }

        return products;
    }
}
