using System.Diagnostics;
using DepthSheetCreator.Enums;

namespace DepthSheetCreator.Models.SportPlayerModels;

[DebuggerDisplay("{Name.FirstName} {Name.LastName}")]
public class MlbPlayer : IPlayer
{
    public string PlayerNumber { get; set; }
    public Name Name { get; set; }
    public PreferredArm BattingArm { get; set; }
    public PreferredArm ThrowingArm { get; set; }

    public MlbPlayer(string playerNumber, string firstName, string lastName, PreferredArm battingArm, PreferredArm throwingArm)
    {
        PlayerNumber = playerNumber;
        Name = new Name(firstName, lastName);
        BattingArm = battingArm;
        ThrowingArm = throwingArm;
    }
    
    public string GetPlayerDetailsString()
    {
        var playerDetails = $"(#{PlayerNumber}, {Name.FirstName} {Name.LastName}, ({BattingArm}, {ThrowingArm}))";

        return playerDetails;
    }
}