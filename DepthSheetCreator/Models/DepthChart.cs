using System.Text;
using DepthSheetCreator.Interfaces;

namespace DepthSheetCreator.Models;

public class DepthChart<TPosition, TPlayer> : IDepthChart<TPosition, TPlayer> 
    where TPosition : Enum
    where TPlayer : IPlayer
{
    private Dictionary<TPosition, List<TPlayer>> _depthChart { get; } = new();

    public void AddPlayerToDepthChart(TPosition position, TPlayer playerToInsert, int? positionDepth = null)
    {
        if(position is null || playerToInsert is null) return;
            
        var positionExists =  _depthChart.TryGetValue(position, out var players);
            
        if (positionExists && players != null)
        {
            //Remove duplicates just in case
            players.RemoveAll(player => player.PlayerNumber.Equals(playerToInsert.PlayerNumber));
                
            if (!positionDepth.HasValue)
            {
                players.Add(playerToInsert);
                return;
            }
                
            players.Insert(positionDepth.Value, playerToInsert);
            return;
        }
            
        //Position doesn't exist, so add it
        _depthChart.Add(position, new List<TPlayer>() {  playerToInsert });
    }

    public List<TPlayer> RemovePlayerFromDepthChart(TPosition position, TPlayer playerToRemove)
    {
        var positionExists =  _depthChart.TryGetValue(position, out var players);
            
        if(!positionExists || players is null || playerToRemove is null) return new List<TPlayer>();

        var existingPlayer = players.FirstOrDefault(x => x.PlayerNumber.Equals(playerToRemove.PlayerNumber));
            
        if(existingPlayer is null) return new List<TPlayer>();
            
        players.Remove(existingPlayer);

        return new List<TPlayer>() { existingPlayer };
    }

    public List<TPlayer> GetBackUps(TPosition position, TPlayer player)
    {
        var positionExists =  _depthChart.TryGetValue(position, out var players);
            
        //There are no players, or there is only 1 player and therefore no backups
        if(!positionExists || players is null || players.Count < 1) return new List<TPlayer>();
            
        var existingPlayerIndex = players.FindIndex(x => x.PlayerNumber.Equals(player.PlayerNumber));
            
        if(existingPlayerIndex == -1) return new List<TPlayer>();

        var backUpPlayers = players.Skip(existingPlayerIndex + 1).ToList();
            
        return backUpPlayers;
    }

    public virtual void GetFullDepthChart()
    {
        var stringBuilder = new StringBuilder();

        foreach (var position in _depthChart.Keys)
        {
            stringBuilder.Append($"{position.ToString()} - ");
                
            var playersExist =  _depthChart.TryGetValue(position, out var players);

            if (!playersExist || players is null || players.Count == 0)
            {
                stringBuilder.AppendLine("No Players");
                continue;
            }

            var playerDetailStrings = players.Select(player => player.GetPlayerDetailsString());
                
            stringBuilder.AppendLine(string.Join(", ", playerDetailStrings));
        }
            
        var stringResult = stringBuilder.ToString();

        Console.WriteLine(stringResult);
    }

    public List<TPlayer> GetPlayersAtPosition(TPosition position)
    {
        var positionExists =  _depthChart.TryGetValue(position, out var players);
            
        if(!positionExists || players is null || players.Count == 0) return new List<TPlayer>();
            
        return players;
    }
}