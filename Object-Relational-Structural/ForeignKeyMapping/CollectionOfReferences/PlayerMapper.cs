using System.Data;

namespace ForeignKeyMapping.CollectionOfReferences;

public class PlayerMapper : AbstractMapper<Player>
{
    protected override string TableName => "Players";

    protected override Player CreateDomainObject()
    {
        return new Player();
    }

    protected override void DoLoad(Player domainObject, DataRow row)
    {
        throw new NotImplementedException();
    }

    public ICollection<Player> FindForTeam(int teamId)
    {
        var filter = $"teamId = {teamId}";
        var rows = Table.Select(filter);

        var result = rows.Select(row => Load(row)).ToList();
        return result;
    }

    protected override void Save(Player domainObject, DataRow row)
    {
        throw new NotImplementedException();
    }

    public void LinkTeam(Player player, int teamId)
    {
        var row = FindRow(player.Id);
        row["teamId"] = teamId;
    }
}