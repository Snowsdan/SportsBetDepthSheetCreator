using DepthSheetCreator.Interfaces;

namespace DepthSheetCreator.Models;

public class Sport<TPosition, TPlayer> : ISport<TPosition, TPlayer>
    where TPosition : Enum
    where TPlayer : IPlayer
{
    public string Name { get; set; }
    public List<ITeam<TPosition, TPlayer>> Teams { get; set; }

    public Sport(string sportName, List<ITeam<TPosition, TPlayer>> teams)
    {
        Name = sportName;
        Teams = teams;
    }
}