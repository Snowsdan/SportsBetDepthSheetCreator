using DepthSheetCreator.Interfaces;

namespace DepthSheetCreator.Models;

public interface IPlayer
{
    public string PlayerNumber { get; set; }
    public Name Name { get; set; }
    public string GetPlayerDetailsString();
}