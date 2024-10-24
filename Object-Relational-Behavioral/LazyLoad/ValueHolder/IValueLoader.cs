namespace LazyLoad.ValueHolder;

// Interface to load object from external resource
public interface IValueLoader<T>
{
    T Load();
}