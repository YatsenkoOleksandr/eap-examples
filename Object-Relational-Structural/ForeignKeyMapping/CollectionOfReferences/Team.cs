namespace ForeignKeyMapping.CollectionOfReferences;

/// <summary>
/// Domain class with foreign key link one-to-many
/// </summary>
public class Team: DomainObject
{
    private ICollection<Player> players = [];

    public string Name { get; set; } = string.Empty;

    public ICollection<Player> Players
    {
        get { return players.ToArray().AsReadOnly(); }
        set { players = value; }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
}