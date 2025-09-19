using DepthSheetCreator.Models;

namespace DepthSheetCreator.Interfaces;

public interface IDepthChart<TPosition, TPlayer>
    where TPosition : Enum
    where TPlayer : IPlayer
{
    public void AddPlayerToDepthChart(TPosition position, TPlayer player, int? positionDepth = null);
    public List<TPlayer> RemovePlayerFromDepthChart(TPosition position, TPlayer player);
    public List<TPlayer> GetBackUps(TPosition position, TPlayer player);
    public void GetFullDepthChart();
}