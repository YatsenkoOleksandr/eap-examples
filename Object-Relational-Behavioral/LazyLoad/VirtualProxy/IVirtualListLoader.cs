namespace LazyLoad.VirtualProxy;

/// <summary>
/// Generic interface to load collection from external resource
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVirtualListLoader<T>
{
    /// <summary>
    /// Loads collection from external resource
    /// </summary>
    /// <returns></returns>
    IEnumerable<T> Load();
}