using System.Diagnostics;

namespace LazyLoad.Ghost;

/// <summary>
/// Domain object Layer Supertype
/// </summary>
public abstract class DomainObject
{
    protected LoadStatus status;

    public int Key { get; protected set; }

    public DomainObject(int key)
    {
        Key = key;
    }

    public bool IsGhost
    {
        get { return status == LoadStatus.Ghost; }
    }

    public bool IsLoaded
    {
        get { return status == LoadStatus.Loaded; }
    }

    public void MarkAsLoading()
    {
        Debug.Assert(IsGhost);
        status = LoadStatus.Loading;
    }

    public void MarkAsLoaded()
    {
        Debug.Assert(status == LoadStatus.Loading);
        status = LoadStatus.Loaded;
    }

    /// <summary>
    /// Load domain object if it is a ghost
    /// </summary>
    protected virtual void Load()
    {
        if (IsGhost)
        {
            DataSource.Load(this);
        }
    }
}