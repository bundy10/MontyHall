
using MontyHallv2.DoorCreation;
using MontyHallv2.GameShowStaff;
using MontyHallv2.Interfaces;
using MontyHallv2.Random;

namespace MontyHallv2.GameModes;

public class SimulatorGame : IGameMode
{
    private readonly IStrategy _strategy;
    private readonly IRandom _random;

    public SimulatorGame(IStrategy strategy, IRandom randomNum)
    {
        _strategy = strategy;
        _random = randomNum;
    }

    public void PlayerChooseDoor(List<Door> doors)
    {
        doors[_random.GetNumberBetweenRange(0, doors.Count)].PlayerPickedDoor();
    }

    public void PlayerSwitchOrStayDoor(List<Door> doors)
    {
        _strategy.ToSwitchOrStay(doors);
    }

    public bool GetGameOutCome(List<Door> doors)
    {
        return doors.First(door => door.HasPlayerPicked()).HasCar();
    }
}