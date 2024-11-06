using System.Data;

namespace ForeignKeyMapping.CollectionOfReferences;

public class TeamMapper: AbstractMapper<Team>
{
    protected override string TableName => "Teams";

    protected override Team CreateDomainObject()
    {
        return new Team();
    }

    /// <summary>
    /// Loads <see cref="Team"/> and its <see cref="Player"/>
    /// </summary>
    /// <param name="team"></param>
    /// <param name="row"></param>
    protected override void DoLoad(Team team, DataRow row)
    {
        team.Name = (string) row["name"];
        team.Players = FindForTeam(team.Id);
    }

    protected ICollection<Player> FindForTeam(int teamId)
    {
        // return MapperRegistry.Player.FindForTeam(teamId);
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates <see cref="Team"/> and links in its <see cref="Player"/>
    /// </summary>
    /// <param name="team"></param>
    /// <param name="row"></param>
    protected override void Save(Team team, DataRow row)
    {
        row["name"] = team.Name;
        SavePlayers(team);
    }

    private void SavePlayers(Team team)
    {
        foreach (Player player in team.Players)
        {
            LinkTeam(player, team.Id);
        }
    }

    private void LinkTeam(Player player, int teamId)
    {
        // MapperRegistry.Player.LinkTeam(player, team.Id);
    }
}