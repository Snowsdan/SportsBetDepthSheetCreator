using DepthSheetCreator.Models;

namespace DepthSheetCreator.Interfaces;

public interface ISport<TPosition, TPlayer> 
    where TPosition : Enum
    where TPlayer : IPlayer
{
    public string Name { get; set; }
    public List<ITeam<TPosition, TPlayer>> Teams { get; set; }
}