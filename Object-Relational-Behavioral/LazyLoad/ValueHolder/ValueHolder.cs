namespace LazyLoad.ValueHolder;

public class ValueHolder<T>
{
    private bool isLoaded = false;
    private T value;

    private readonly IValueLoader<T> loader;

    public ValueHolder(IValueLoader<T> loader)
    {
        this.loader = loader;
    }

    public T Value
    {
        get
        {
            if (!isLoaded)
            {
                value = loader.Load();
                isLoaded = true;
            }

            return value;
        }
    }
}