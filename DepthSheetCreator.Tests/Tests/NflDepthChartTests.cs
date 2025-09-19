using DepthSheetCreator.Enums.PlayerPositions;
using DepthSheetCreator.Models;
using DepthSheetCreator.Models.SportPlayerModels;
using NUnit.Framework;

namespace DepthSheetCreator.Tests.Tests;

[TestFixture]
public class NflDepthChartTests
{
    private DepthChart<NflPosition, NflPlayer> _depthChart = new();
    private readonly NflPlayer _tomBrady = new NflPlayer("12", "Tom", "Brady");
    private readonly NflPlayer _blaineGabbert = new NflPlayer("11", "Blaine", "Gabbert");
    private readonly NflPlayer _kyleTrask = new NflPlayer("2", "Kyle", "Trask");

    [SetUp]
    public void SetUp()
    {
        _depthChart = new DepthChart<NflPosition, NflPlayer>();
    }

    [Test]
    public void AddPlayerToDepthChart_AddsNewPosition_WhenDepthChartIsEmpty()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players, Is.Not.Empty);
        Assert.That(players.First().PlayerNumber, Is.EqualTo(_tomBrady.PlayerNumber));
    }

    [Test]
    public void AddPlayerToDepthChart_AppendsPlayer_WhenPositionExists_AndNoPositionDepth()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);

        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players.Count, Is.EqualTo(2));
        Assert.That(players.First().PlayerNumber, Is.EqualTo(_tomBrady.PlayerNumber));
        Assert.That(players.Last().PlayerNumber, Is.EqualTo(_blaineGabbert.PlayerNumber));
    }

    [Test]
    public void AddPlayerToDepthChart_RemovesDuplicate_WithNoPositionDepth()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);

        // Insert duplicate Tom Brady
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players.Count, Is.EqualTo(2));
        Assert.That(players.First().PlayerNumber, Is.EqualTo(_blaineGabbert.PlayerNumber));
        Assert.That(players.Last().PlayerNumber, Is.EqualTo(_tomBrady.PlayerNumber));
    }
    
    [Test]
    public void AddPlayerToDepthChart_InsertsAtSpecificDepth_WithExistingPlayers()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);

        var positionDepth = 1;
        //Insert between _tomBrady and _blaineGabbert
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _kyleTrask, positionDepth);

        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players.Count, Is.EqualTo(3));
        Assert.That(players.First().PlayerNumber, Is.EqualTo(_tomBrady.PlayerNumber));
        Assert.That(players[positionDepth].PlayerNumber, Is.EqualTo(_kyleTrask.PlayerNumber));
        Assert.That(players.Last().PlayerNumber, Is.EqualTo(_blaineGabbert.PlayerNumber));
    }
    
    [Test]
    public void AddPlayerToDepthChart_InsertsAtSpecificDepth_WithNoExistingPlayers()
    {
        var positionDepth = 1;
        
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _kyleTrask, positionDepth);

        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players.Count, Is.EqualTo(1));
        Assert.That(players.First().PlayerNumber, Is.EqualTo(_kyleTrask.PlayerNumber));
    }

    [Test]
    public void RemovePlayerFromDepthChart_ReturnsEmptyList_WhenPositionDoesntExist()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var removed = _depthChart.RemovePlayerFromDepthChart(NflPosition.C, _tomBrady);

        Assert.That(removed.Count, Is.EqualTo(0));
    }

    [Test]
    public void RemovePlayerFromDepthChart_RemovesExistingPlayer()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);

        var removed = _depthChart.RemovePlayerFromDepthChart(NflPosition.QB, _tomBrady);

        Assert.That(removed.Count, Is.EqualTo(1));
        Assert.That(removed.First().PlayerNumber, Is.EqualTo(_tomBrady.PlayerNumber));

        var remaining = _depthChart.GetPlayersAtPosition(NflPosition.QB);
        Assert.That(remaining.Count, Is.EqualTo(1));
        Assert.That(remaining.First().PlayerNumber, Is.EqualTo(_blaineGabbert.PlayerNumber));
    }

    [Test]
    public void RemovePlayerFromDepthChart_ReturnsEmpty_WhenPlayerNotFound()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var removed = _depthChart.RemovePlayerFromDepthChart(NflPosition.QB, _blaineGabbert);

        Assert.That(removed, Is.Empty);
    }

    [Test]
    public void GetBackUps_ReturnsPlayers_WhenGivenPlayer_AtBeginningOfChart()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _kyleTrask);

        var backups = _depthChart.GetBackUps(NflPosition.QB, _tomBrady);

        Assert.That(backups.Count, Is.EqualTo(2));
        Assert.That(backups.First().PlayerNumber, Is.EqualTo(_blaineGabbert.PlayerNumber));
    }
    
    [Test]
    public void GetBackUps_ReturnsPlayers_WhenGivenPlayer_InMiddleOfChart()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _kyleTrask);

        var backups = _depthChart.GetBackUps(NflPosition.QB, _blaineGabbert);

        Assert.That(backups.Count, Is.EqualTo(1));
        Assert.That(backups.First().PlayerNumber, Is.EqualTo(_kyleTrask.PlayerNumber));
    }
    
    [Test]
    public void GetBackUps_ReturnsPlayers_WhenGivenPlayer_AtEndOfChart()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _blaineGabbert);
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _kyleTrask);

        var backups = _depthChart.GetBackUps(NflPosition.QB, _kyleTrask);

        Assert.That(backups.Count, Is.EqualTo(0));
    }

    [Test]
    public void GetBackUps_ReturnsEmptyList_WhenPlayerNotFound()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var backups = _depthChart.GetBackUps(NflPosition.QB, _blaineGabbert);

        Assert.That(backups, Is.Empty);
    }
    
    [Test]
    public void GetBackUps_ReturnsEmptyList_WhenPositionNotFound()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var backups = _depthChart.GetBackUps(NflPosition.DE, _blaineGabbert);

        Assert.That(backups, Is.Empty);
    }
    
    [Test]
    public void GetBackUps_ReturnsEmptyList_WhenPlayerHasNoBackups()
    {
        _depthChart.AddPlayerToDepthChart(NflPosition.QB, _tomBrady);

        var backups = _depthChart.GetBackUps(NflPosition.QB, _tomBrady);

        Assert.That(backups, Is.Empty);
    }

    [Test]
    public void GetPlayersAtPosition_ReturnsEmpty_WhenNoPlayersExist()
    {
        var players = _depthChart.GetPlayersAtPosition(NflPosition.QB);

        Assert.That(players, Is.Empty);
    }
}
