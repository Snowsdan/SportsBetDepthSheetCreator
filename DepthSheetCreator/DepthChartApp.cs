using DepthSheetCreator.Enums;
using DepthSheetCreator.Enums.PlayerPositions;
using DepthSheetCreator.Interfaces;
using DepthSheetCreator.Models;
using DepthSheetCreator.Models.SportPlayerModels;

namespace DepthSheetCreator;

public class DepthChartApp
{
    public void Run()
    {
        CreateNflTeams();
        
        Console.WriteLine("------------------------------------------");

        CreateMlbTeams();
    }

    private void CreateMlbTeams()
    {
        Console.WriteLine($"Creating Los Angeles Dodgers with the following players:");
        
        //Create players
        var willSmith = new MlbPlayer("16", "Will", "Smith", PreferredArm.Right, PreferredArm.Right);
        var benRortvedt = new MlbPlayer("47", "Ben", "Rortvedt", PreferredArm.Left, PreferredArm.Right);
        var daltonRushing = new MlbPlayer("68", "Dalton", "Rushing", PreferredArm.Left, PreferredArm.Right);
        var tannerScott = new MlbPlayer("66", "Tanner", "Scott", PreferredArm.Right, PreferredArm.Left);
        var alexVesia = new MlbPlayer("51", "Alex", "Vesia", PreferredArm.Left, PreferredArm.Left);
        
        var laDodgersPlayerDetails = GetPlayersDetails(new[] { willSmith, benRortvedt, daltonRushing, tannerScott, alexVesia });
        Console.WriteLine(laDodgersPlayerDetails);

        //Create team and assign it to a sport
        var laDodgers = new Team<MlbPosition, MlbPlayer>("Los Angeles Dodgers", new DepthChart<MlbPosition, MlbPlayer>());
        var mlb = new Sport<MlbPosition, MlbPlayer>("MLB", new List<ITeam<MlbPosition, MlbPlayer>>() { laDodgers });
        
        //Add players to Depth Chart
        laDodgers.DepthChart.AddPlayerToDepthChart(MlbPosition.Catcher, willSmith);
        laDodgers.DepthChart.AddPlayerToDepthChart(MlbPosition.Catcher, benRortvedt);
        laDodgers.DepthChart.AddPlayerToDepthChart(MlbPosition.Catcher, daltonRushing);

        laDodgers.DepthChart.AddPlayerToDepthChart(MlbPosition.Bullpen, tannerScott);
        laDodgers.DepthChart.AddPlayerToDepthChart(MlbPosition.Bullpen, alexVesia);

        Console.WriteLine($"{Environment.NewLine}Current Depth Chart:");
        PrintTeamsDepthCharts(mlb.Teams);
        
        //Get back ups
        var benRortvedtBackups = laDodgers.DepthChart.GetBackUps(MlbPosition.Catcher, benRortvedt);
        Console.WriteLine($"Catcher Backups for Ben Rortvedt: {GetPlayersDetails(benRortvedtBackups)}");
        
        //Remove players from Depth Chart
        Console.WriteLine("Removing Will Smith and Ben RortVedt (Catchers)");
        laDodgers.DepthChart.RemovePlayerFromDepthChart(MlbPosition.Bullpen, tannerScott);
        laDodgers.DepthChart.RemovePlayerFromDepthChart(MlbPosition.Bullpen, alexVesia);
        
        //Display updated Depth Chart
        Console.WriteLine($"{Environment.NewLine}Updated Depth Chart:");
        PrintTeamsDepthCharts(mlb.Teams);
    }
    
    private void CreateNflTeams()
    {
        Console.WriteLine($"Creating Tampa Bay Buccaneers with the following players:");
        
        //Create Players for Tampa Bay
        var tomBrady = new NflPlayer("12", "Tom", "Brady");
        var blaineGabbert = new NflPlayer("11", "Blaine", "Gabbert");
        var kyleTrask = new NflPlayer("2", "Kyle", "Trask");
        var mikeEvans = new NflPlayer("13", "Mike", "Evans");
        var jaelonDarden = new NflPlayer("1", "Jaelon", "Darden");
        var scottMiller = new NflPlayer("10", "Scott", "Miller");

        var tampaBayPlayerDetails = GetPlayersDetails(new[] { tomBrady, blaineGabbert, kyleTrask, mikeEvans, jaelonDarden, scottMiller });
        Console.WriteLine(tampaBayPlayerDetails + Environment.NewLine);
        
        //Create Tampa Bay team
        var tampaBayBuccaneers = new Team<NflPosition, NflPlayer>("Tampa Bay Buccaneers", new DepthChart<NflPosition, NflPlayer>());
        
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.QB, tomBrady);
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.QB, blaineGabbert);
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.QB, kyleTrask);
        
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.LWR, mikeEvans);
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.LWR, jaelonDarden);
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.LWR, scottMiller);
        tampaBayBuccaneers.DepthChart.AddPlayerToDepthChart(NflPosition.LWR, tomBrady);
        
        Console.WriteLine($"Creating Arizona Cardinals with the following players:");
        
        //Create Arizona Cardinals players
        var michaelWilson = new NflPlayer("14", "Michael", "Wilson");
        var gregDortch = new NflPlayer("4", "Greg", "Dortch");
        var treyMcBride = new NflPlayer("85", "Trey", "Bride");
        var tipReiman = new NflPlayer("87", "Tip", "Reiman");
        var elijahHiggins = new NflPlayer("84", "Elijah", "Higgins");
        var travisVokolek = new NflPlayer("81", "Travis", "Vokolek");

        var arizonaCardinalsPlayerDetails = GetPlayersDetails(new[]
            { michaelWilson, gregDortch, treyMcBride, tipReiman, elijahHiggins, travisVokolek });
        Console.WriteLine(arizonaCardinalsPlayerDetails);

        //Create Arizona Cardinals team
        var arizonaCardinals =
            new Team<NflPosition, NflPlayer>("Arizona Cardinals", new DepthChart<NflPosition, NflPlayer>());

        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.SWR, michaelWilson);
        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.SWR, gregDortch);
        
        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.TE, treyMcBride);
        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.TE, tipReiman);
        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.TE, elijahHiggins);
        arizonaCardinals.DepthChart.AddPlayerToDepthChart(NflPosition.TE, travisVokolek);
        
        //Assign the teams to a new sport
        var nflTeams = new List<ITeam<NflPosition, NflPlayer>>() { tampaBayBuccaneers, arizonaCardinals };
        var nfl = new Sport<NflPosition, NflPlayer>("NFL", nflTeams);
        
        Console.WriteLine($"{Environment.NewLine}Current Depth Charts:");
        PrintTeamsDepthCharts(nfl.Teams);
        
        //Display backups
        var tomBradyBackupsQb = tampaBayBuccaneers.DepthChart.GetBackUps(NflPosition.QB, tomBrady);
        Console.WriteLine($"QB Backups for Tom Brady: {GetPlayersDetails(tomBradyBackupsQb)}");
        
        var jaelonDardenBackups = tampaBayBuccaneers.DepthChart.GetBackUps(NflPosition.LWR, jaelonDarden);
        Console.WriteLine($"LWR Backups for Jaelon Darden: {GetPlayersDetails(jaelonDardenBackups)}");
        
        var mikeEvansBackups = tampaBayBuccaneers.DepthChart.GetBackUps(NflPosition.QB, mikeEvans);
        Console.WriteLine($"QB Backups for Mike Evans: {GetPlayersDetails(mikeEvansBackups)}");
        
        //Remove a player from each team
        Console.WriteLine($"Removing Jaelon Darden (Tampa Bay) from LWR...");
        tampaBayBuccaneers.DepthChart.RemovePlayerFromDepthChart(NflPosition.LWR, jaelonDarden);
        Console.WriteLine($"Removing Michael Wilson (Arizona Cardinals) from SWR... {Environment.NewLine}");
        arizonaCardinals.DepthChart.RemovePlayerFromDepthChart(NflPosition.SWR, michaelWilson);
        
        Console.WriteLine("Depth charts for all teams:");
        PrintTeamsDepthCharts(nfl.Teams);
    }
    
    private string GetPlayersDetails(IReadOnlyCollection<IPlayer> players)
    {
        if(!players.Any()) return "No players found";
        
        var playerDetails = players.Select(backup => backup.GetPlayerDetailsString());
        var playerDetailsString = string.Join(", ", playerDetails);
        
        return playerDetailsString;
    }

    private void PrintTeamsDepthCharts<TPosition,TPlayer>(IEnumerable<ITeam<TPosition, TPlayer>> teams)
        where TPosition : Enum
        where TPlayer : IPlayer
    {
        foreach (var team in teams)
        {
            Console.WriteLine($"Team: {team.Name}");
            team.DepthChart.GetFullDepthChart();
        }
    }
}