using DepthSheetCreator.Interfaces;

namespace DepthSheetCreator.Models;

public class Team<TPosition, TPlayer> : ITeam<TPosition, TPlayer> 
    where TPosition : Enum
    where TPlayer : IPlayer
{
    public string Name { get; set; }
    public IDepthChart<TPosition, TPlayer> DepthChart { get; set; }

    public Team(string teamName,  IDepthChart<TPosition, TPlayer> depthChart)
    {
        Name = teamName;
        DepthChart = depthChart;
    }
}