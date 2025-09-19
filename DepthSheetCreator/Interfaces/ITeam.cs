using DepthSheetCreator.Models;

namespace DepthSheetCreator.Interfaces;

public interface ITeam<TPosition, TPlayer>
    where TPosition : Enum
    where TPlayer : IPlayer
{
    public string Name { get; set; }
    public IDepthChart<TPosition, TPlayer> DepthChart { get; set; }
}