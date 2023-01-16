using MontyHallv2.DoorCreation;
using MontyHallv2.Interfaces;

namespace MontyHallv2.GameShowStaff;

public class GamePlay
{
    private readonly IGameMode _gameMode;
    private List<Door> _doors = new ();
    private readonly Host _host;
    private readonly GameMaster _gameMaster;

    public GamePlay(IGameMode gameMode, IRandom random)
    {
        _gameMode = gameMode;
        _host = new Host();
        _gameMaster = new GameMaster(random);
    }
    

    public void PlayGame()
    {
        _doors = _gameMaster.GetDoorsIncludingCarDoor();
        _gameMode.PlayerChooseDoor(_doors);
        _host.HostOpensDoor(_doors);
        _gameMode.PlayerSwitchOrStayDoor(_doors);
        GetOutComeOfGame();
    }

    public bool GetOutComeOfGame()
    {
        return _gameMode.GetGameOutCome(_doors);
    }
    
}