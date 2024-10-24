namespace LazyLoad.ValueHolder;

public class ProductLoader : IValueLoader<Product>
{
    public Product Load()
    {
        /*
            return ProductMapper.create().findForSupplier(id);

            return in-memory object instead of call to external resource
        */
        return new Product()
        {
            Id = 7,
        };
    }
}