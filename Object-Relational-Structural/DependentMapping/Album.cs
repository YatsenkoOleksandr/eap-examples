namespace DependentMapping;

/// <summary>
/// Owner object
/// </summary>
public class Album: IDomainObject<long>
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Collection of dependents
    /// </summary>
    public IEnumerable<Track> Tracks = [];
}