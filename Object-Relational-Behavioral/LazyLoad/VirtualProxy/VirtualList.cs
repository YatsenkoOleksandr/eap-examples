using System.Collections;

namespace LazyLoad.VirtualProxy;

/// <summary>
/// Generic Virtual Proxy for <see cref="IEnumerable{T}"/> which loads data on the first access
/// </summary>
/// <typeparam name="T"></typeparam>
public class VirtualList<T>: IEnumerable<T>
{
    private List<T>? source = null;

    private readonly IVirtualListLoader<T> listLoader;

    public VirtualList(IVirtualListLoader<T> listLoader)
    {
        this.listLoader = listLoader;
    }

    /// <summary>
    /// Loads data or returns cached
    /// </summary>
    /// <returns></returns>
    private List<T> GetSource()
    {
        if (source is null)
        {
            source = listLoader.Load().ToList();
        }
        return source;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return GetSource().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetSource().GetEnumerator();
    }
}