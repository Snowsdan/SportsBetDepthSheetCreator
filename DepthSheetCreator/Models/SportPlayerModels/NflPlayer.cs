using System.Diagnostics;

namespace DepthSheetCreator.Models.SportPlayerModels;

[DebuggerDisplay("{Name.FirstName} {Name.LastName}")]
public class NflPlayer : IPlayer
{
    public string PlayerNumber { get; set; }
    public Name Name { get; set; }

    public NflPlayer(string playerNumber, string firstName, string lastName)
    {
        PlayerNumber = playerNumber;
        Name = new Name(firstName, lastName);
    }
    
    public string GetPlayerDetailsString()
    {
        var playerDetails = $"(#{PlayerNumber}, {Name.FirstName} {Name.LastName})";

        return playerDetails;
    }
}